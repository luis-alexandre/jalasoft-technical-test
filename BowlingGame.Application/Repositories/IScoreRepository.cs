using BowlingGame.Domain.Entities;
using System.Threading.Tasks;

namespace BowlingGame.Application.Repositories
{
    public interface IScoreRepository
    {
        Task AddAsync(ScoreEntity scoreEntity);
    }
}
