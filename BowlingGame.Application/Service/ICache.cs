using System;

namespace BowlingGame.Application.Service
{
    public interface ICache<TEntity>
    {
        void Add(string key, TEntity entity, TimeSpan timeSpan);

        bool TryGet(string key, out TEntity data);
    }
}
