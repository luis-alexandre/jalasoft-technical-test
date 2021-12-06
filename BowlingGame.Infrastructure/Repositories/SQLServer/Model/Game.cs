using System;
using System.Collections.Generic;

namespace BowlingGame.Infrastructure.Repositories.SQLServer.Model
{
    public class Game
    {
        public Guid Id { get; set; }

        public string PlayerName { get; set; }

        public ICollection<Score> Scores { get; set; }
    }
}
