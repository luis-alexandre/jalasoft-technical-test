using System;
using System.Threading.Tasks;

namespace BowlingGame.Infrastructure.Repositories.MongoDb
{
    public interface IMongoDbContext<TEntity> where TEntity : MongoEntity
    {
        Task AddAsync(TEntity entity);

        Task<TEntity> GetById(Guid id);
    }
}
