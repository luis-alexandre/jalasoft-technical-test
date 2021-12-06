using BowlingGame.Application.Repositories;
using BowlingGame.Application.UseCases.CreateGame;
using FluentAssertions;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace BowlingGame.Application.UnitTests.UseCases
{
    public class CreateGameUseCaseTests
    {
        private Mock<IGameRepository> gameRespositoryMock;

        private readonly CreateGameUseCase createGameUseCase;

        public CreateGameUseCaseTests()
        {
            gameRespositoryMock = new Mock<IGameRepository>();

            createGameUseCase = new CreateGameUseCase(gameRespositoryMock.Object);
        }

        [Fact]
        public async Task GivenNewPlayer_WhenAddIt_MustCreateNewGame()
        {
            const string playerName = "User001";

            var response = await createGameUseCase.Handle(new CreateGameRequest
            {
                PlayerName = playerName
            }, new CancellationToken());

            response.Should().NotBeNull();
            response.GameId.Should().NotBe(Guid.Empty);
        }
    }
}
