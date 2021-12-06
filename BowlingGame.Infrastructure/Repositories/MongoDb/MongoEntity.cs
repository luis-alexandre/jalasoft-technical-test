using MongoDB.Bson.Serialization.Attributes;
using System;

namespace BowlingGame.Infrastructure.Repositories.MongoDb
{
    public abstract class MongoEntity
    {
        [BsonId]
        public Guid Id { get; set; }
    }
}
