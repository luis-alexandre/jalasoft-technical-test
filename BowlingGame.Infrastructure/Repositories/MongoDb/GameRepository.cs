using AutoMapper;
using BowlingGame.Application.Repositories;
using BowlingGame.Domain.Entities;
using BowlingGame.Infrastructure.Repositories.MongoDb.Model;
using MongoDB.Bson;
using System;
using System.Threading.Tasks;

namespace BowlingGame.Infrastructure.Repositories.MongoDb
{
    public class GameRepository : IGameRepository
    {
        private readonly IMongoDbContext<Game> _context;

        private static readonly IMapper Mapper = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<GameEntity, Game>().ReverseMap();
            cfg.CreateMap<ScoreEntity, Score>().ReverseMap();

            cfg.AllowNullCollections = true;

        }).CreateMapper();

        public GameRepository(IMongoDbContext<Game> context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task AddAsync(GameEntity gameEntity)
        {
            var game = Mapper.Map<Game>(gameEntity);
            await _context.AddAsync(game);
        }

        public async Task<GameEntity> FindByIdAsync(Guid id)
        {
            var game = await _context.GetById(id);

            if (game == null)
            {
                return null;
            }

            var gameEntity = Mapper.Map<GameEntity>(game);
            return gameEntity;
        }
    }
}
