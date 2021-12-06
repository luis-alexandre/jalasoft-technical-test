using BowlingGame.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace BowlingGame.Application.Repositories
{
    public interface IGameRepository
    {
        Task AddAsync(GameEntity gameEntity);

        Task<GameEntity> FindByIdAsync(Guid id);
    }
}
