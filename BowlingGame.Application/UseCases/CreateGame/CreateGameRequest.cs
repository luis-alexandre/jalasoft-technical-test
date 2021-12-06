using BowlingGame.Domain.Entities;
using MediatR;
using System;

namespace BowlingGame.Application.UseCases.CreateGame
{
    public class CreateGameRequest : IRequest<CreateGameResponse>
    {
        public string PlayerName { get; set; }

        public GameEntity ToGameEntity()
        {
            return new GameEntity
            {
                Id = Guid.NewGuid(),
                PlayerName = PlayerName
            };
        }        
    }
}
