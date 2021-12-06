using BowlingGame.Infrastructure.Repositories.SQLServer.Model;
using Microsoft.EntityFrameworkCore;

namespace BowlingGame.Infrastructure.Repositories.SQLServer
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) 
            : base(options)
        {
        }

        public DbSet<Game> Games { get; set; }
        public DbSet<Score> Scores { get; set; }
    }
}
