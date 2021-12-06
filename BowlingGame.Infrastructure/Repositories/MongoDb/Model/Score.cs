using System;

namespace BowlingGame.Infrastructure.Repositories.MongoDb.Model
{
    [Serializable]
    public class Score : MongoEntity
    {
        public int Value { get; set; }
    }
}
