using AutoMapper;
using BowlingGame.Application.Repositories;
using BowlingGame.Domain.Entities;
using BowlingGame.Infrastructure.Repositories.SQLServer.Model;
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BowlingGame.Infrastructure.Repositories.SQLServer
{
    public class GameRepository : IGameRepository
    {
        private readonly AppDbContext _context;

        private static readonly IMapper Mapper = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<GameEntity, Game>().ReverseMap();
            cfg.CreateMap<ScoreEntity, Score>().ReverseMap();

            cfg.AllowNullCollections = true;

        }).CreateMapper();

        public GameRepository(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task AddAsync(GameEntity gameEntity)
        {
            var game = Mapper.Map<Game>(gameEntity);
            _context.Games.Add(game);
            
            await _context.SaveChangesAsync();
        }

        public async Task<GameEntity> FindByIdAsync(Guid id)
        {
            var game = await _context.Games.Include(x => x.Scores)
                                           .FirstOrDefaultAsync(x => x.Id.Equals(id));

            if(game == null)
            {
                return null;
            }

            var gameEntity = Mapper.Map<GameEntity>(game);
            return gameEntity;
        }
    }
}
