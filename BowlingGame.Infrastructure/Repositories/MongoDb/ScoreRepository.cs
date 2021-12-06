using AutoMapper;
using BowlingGame.Application.Repositories;
using BowlingGame.Domain.Entities;
using BowlingGame.Infrastructure.Repositories.MongoDb.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BowlingGame.Infrastructure.Repositories.MongoDb
{
    public class ScoreRepository : IScoreRepository
    {
        private readonly IMongoDbContext<Game> _context;

        private static readonly IMapper Mapper = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<GameEntity, Game>().ReverseMap();
            cfg.CreateMap<ScoreEntity, Score>().ReverseMap();

            cfg.AllowNullCollections = true;

        }).CreateMapper();

        public ScoreRepository(IMongoDbContext<Game> context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task AddAsync(ScoreEntity scoreEntity)
        {
            var score = Mapper.Map<Score>(scoreEntity);

            var game = await _context.GetById(scoreEntity.GameId);

            if(game.Scores == null)
            {
                game.Scores = new List<Score>();
            }

            game.Scores.Add(score);

            await _context.AddAsync(game);
        }
    }
}
