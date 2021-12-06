using System;

namespace BowlingGame.Infrastructure.Repositories.SQLServer.Model
{
    public  class Score
    {
        public Guid Id { get; set; }

        public Guid GameId { get; set; }

        public virtual Game Game { get; set; }

        public int Value { get; set; }
    }
}
