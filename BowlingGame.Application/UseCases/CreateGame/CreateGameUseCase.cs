using BowlingGame.Application.Repositories;
using BowlingGame.Domain.Exceptions;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BowlingGame.Application.UseCases.CreateGame
{
    public class CreateGameUseCase : IRequestHandler<CreateGameRequest, CreateGameResponse>
    {
        private readonly IGameRepository _gameRepository;

        public CreateGameUseCase(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }

        public async Task<CreateGameResponse> Handle(CreateGameRequest request, CancellationToken cancellationToken)
        {
            CreateGameRequestValidator validations = new();
            var validationResult = validations.Validate(request);

            if (!validationResult.IsValid)
            {
                throw new GameException(validationResult.ToString());
            }

            var gameEntity = request.ToGameEntity();
            
            await _gameRepository.AddAsync(gameEntity);

            return new CreateGameResponse
            { 
                GameId = gameEntity.Id 
            };
        }
    }
}
