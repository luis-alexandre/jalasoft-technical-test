using System;
using System.Collections.Generic;
using System.Linq;

namespace BowlingGame.Domain.Entities
{
    public class GameEntity
    {
        public Guid Id { get; set; }

        public string PlayerName { get; set; }

        public ICollection<ScoreEntity> Scores{ get; set; }

        public int GetTotalScore()
        {
            if(Scores == null)
            {
                return 0;
            }

            return Scores.Sum(x => x.Value);
        }
    }
}
