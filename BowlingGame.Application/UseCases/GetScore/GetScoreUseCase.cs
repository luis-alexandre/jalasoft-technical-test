using BowlingGame.Application.Repositories;
using BowlingGame.Application.Service;
using BowlingGame.Domain.Entities;
using BowlingGame.Domain.Exceptions;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BowlingGame.Application.UseCases.GetScore
{
    public class GetScoreUseCase : IRequestHandler<GetScoreRequest, GetScoreResponse>
    {
        private readonly IGameRepository _gameRepository;
        private readonly ICache<GameEntity> _cache;

        public GetScoreUseCase(IGameRepository gameRepository, ICache<GameEntity> cache)
        {
            _gameRepository = gameRepository;
            _cache = cache;
        }

        public async Task<GetScoreResponse> Handle(GetScoreRequest request, CancellationToken cancellationToken)
        {
            GetScoreRequestValidator validations = new();
            var validationResult = validations.Validate(request);

            if (!validationResult.IsValid)
            {
                throw new GameException(validationResult.ToString());
            }

            if(!_cache.TryGet(request.GameId.ToString(), out GameEntity gameEntity))
            {
                gameEntity = await _gameRepository.FindByIdAsync(request.GameId);
                
                if (gameEntity == null)
                {
                    throw new GameException("Game ID not found.");
                }

                _cache.Add(request.GameId.ToString(), gameEntity, TimeSpan.FromMinutes(5));
            }

            return new GetScoreResponse
            {
                PlayerName = gameEntity.PlayerName,
                Score = gameEntity.GetTotalScore()
            };
        }
    }
}
