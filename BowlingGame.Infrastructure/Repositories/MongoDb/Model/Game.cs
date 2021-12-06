using System;
using System.Collections.Generic;

namespace BowlingGame.Infrastructure.Repositories.MongoDb.Model
{
    [Serializable]
    public class Game : MongoEntity
    {
        public string PlayerName { get; set; }

        public ICollection<Score> Scores { get; set; }
    }
}