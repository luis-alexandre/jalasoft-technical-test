using BowlingGame.Application.Repositories;
using BowlingGame.Application.UseCases.AddScore;
using BowlingGame.Domain.Entities;
using FluentAssertions;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace BowlingGame.Application.UnitTests.UseCases
{
    public class AddScoreUseCaseTests
    {
        private Mock<IGameRepository> gameRespositoryMock;
        private Mock<IScoreRepository> scoreRespositoryMock;

        private readonly AddScoreUseCase addScoreUseCase;

        public AddScoreUseCaseTests()
        {
            gameRespositoryMock = new Mock<IGameRepository>();
            scoreRespositoryMock = new Mock<IScoreRepository>();

            addScoreUseCase = new AddScoreUseCase(gameRespositoryMock.Object, scoreRespositoryMock.Object);
        }

        [Fact]
        public async Task GivenNewScore_WhenAddIt_MustReturnResponse()
        {
            var gameId = Guid.NewGuid();
            const string playerName = "User001";

            gameRespositoryMock.Setup(x => x.FindByIdAsync(It.IsAny<Guid>())).ReturnsAsync(new GameEntity
            {
                Id = gameId,
                PlayerName = playerName
            });

            var response = await addScoreUseCase.Handle(new AddScoreRequest
            {
                GameId = Guid.NewGuid(),
                NumberPins = 10

            }, new CancellationToken());

            response.Should().NotBeNull();
            response.TotalPins.Should().Be(10);
        }
    }
}
