using System;

namespace BowlingGame.Domain.Entities
{
    public class ScoreEntity
    {
        public Guid Id { get; set; }

        public int Value { get; set; }

        public Guid GameId { get; set; }

        public GameEntity Game { get; set; }
    }
}
