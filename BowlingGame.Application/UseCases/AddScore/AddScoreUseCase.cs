using BowlingGame.Application.Repositories;
using BowlingGame.Domain.Entities;
using BowlingGame.Domain.Exceptions;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BowlingGame.Application.UseCases.AddScore
{
    public class AddScoreUseCase : IRequestHandler<AddScoreRequest, AddScoreResponse>
    {
        private readonly IGameRepository _gameRepository;
        private readonly IScoreRepository _scoreRepository;

        private const int NUMBER_PINS = 10;

        public AddScoreUseCase(IGameRepository gameRepository, 
                               IScoreRepository scoreRepository)
        {
            _gameRepository = gameRepository;
            _scoreRepository = scoreRepository;
        }

        public async Task<AddScoreResponse> Handle(AddScoreRequest request, CancellationToken cancellationToken)
        {
            AddScoreRequestValidator validations = new();
            var validationResult = validations.Validate(request);

            if (!validationResult.IsValid)
            {
                throw new GameException(validationResult.ToString());
            }

            var gameEntity = await _gameRepository.FindByIdAsync(request.GameId);

            if (gameEntity == null)
            {
                throw new GameException("Game ID not found.");
            }

            var currentTotal = gameEntity.GetTotalScore();
            var total = currentTotal + request.NumberPins;

            if(total > NUMBER_PINS)
            {
                throw new GameException($"Invalid number of pins. The number of pins remaining is {(NUMBER_PINS - currentTotal)}.");
            }

            await _scoreRepository.AddAsync(new ScoreEntity
            {
                Id = Guid.NewGuid(),
                GameId = gameEntity.Id,
                Value = request.NumberPins
            });

            return new AddScoreResponse
            {
                TotalPins = total
            };
        }
    }
}
