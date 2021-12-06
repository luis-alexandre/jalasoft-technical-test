using BowlingGame.Application.Repositories;
using BowlingGame.Application.Service;
using BowlingGame.Application.UseCases.GetScore;
using BowlingGame.Domain.Entities;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace BowlingGame.Application.UnitTests.UseCases
{
    public class GetScoreUseCaseTests
    {
        private Mock<IGameRepository> gameRespositoryMock;
        private Mock<ICache<GameEntity>> cacheMock;

        private readonly GetScoreUseCase getScoreUseCase;

        public GetScoreUseCaseTests()
        {
            gameRespositoryMock = new Mock<IGameRepository>();
            cacheMock = new Mock<ICache<GameEntity>>();

            getScoreUseCase = new GetScoreUseCase(gameRespositoryMock.Object, cacheMock.Object);
        }

        [Fact]
        public async Task GivenGameId_WhenGetScore_MustReturnCurrentScore()
        {
            Guid gameId = Guid.NewGuid();
            const string playerName = "User001";
            var scoreExpected = new ScoreEntity
            {
                GameId = gameId,
                Value = 1,
                Id = Guid.NewGuid(),
            };

            gameRespositoryMock.Setup(x => x.FindByIdAsync(It.IsAny<Guid>())).ReturnsAsync(new GameEntity
            {
                Id = gameId,
                PlayerName = playerName,
                Scores = new List<ScoreEntity>
                {
                    scoreExpected
                }
            });

            var response = await getScoreUseCase.Handle(new GetScoreRequest
            {
                GameId = gameId
            }, new System.Threading.CancellationToken());

            response.Should().NotBeNull();
            response.PlayerName.Should().Be(playerName);
            response.Score.Should().Be(scoreExpected.Value);
        }
    }
}
