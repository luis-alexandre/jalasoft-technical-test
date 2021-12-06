using AutoMapper;
using BowlingGame.Application.Repositories;
using BowlingGame.Domain.Entities;
using BowlingGame.Infrastructure.Repositories.SQLServer.Model;
using System;
using System.Threading.Tasks;

namespace BowlingGame.Infrastructure.Repositories.SQLServer
{
    public class ScoreRepository : IScoreRepository
    {
        private readonly AppDbContext _context;

        private static readonly IMapper Mapper = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<GameEntity, Game>().ReverseMap();
            cfg.CreateMap<ScoreEntity, Score>().ReverseMap();

            cfg.AllowNullCollections = true;

        }).CreateMapper();

        public ScoreRepository(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task AddAsync(ScoreEntity scoreEntity)
        {
            var score = Mapper.Map<Score>(scoreEntity);
            _context.Scores.Add(score);

            await _context.SaveChangesAsync();
        }
    }
}
