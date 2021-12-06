using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BowlingGame.Infrastructure.Repositories.MongoDb
{
    public class MongDbContext<TEntity> : IMongoDbContext<TEntity> where TEntity : MongoEntity
    {
        public MongDbContext(IMongoClient client, IConfiguration configuration)
        {
            var database = configuration["MongoDbSettings:Instance"];
            DbSet = client.GetDatabase(database).GetCollection<TEntity>(typeof(TEntity).Name);
        }

        protected readonly IMongoCollection<TEntity> DbSet;

        public async Task AddAsync(TEntity entity)
        {
            var filter = Builders<TEntity>.Filter.Eq("_id", entity.Id);
            await DbSet.ReplaceOneAsync(filter, entity, new ReplaceOptions { IsUpsert = true });
        }

        public async Task<TEntity> GetById(Guid id)
        {
            var data = await DbSet.FindAsync(Builders<TEntity>.Filter.Eq("_id", id));
            return data.FirstOrDefault();
        }
    }
}
