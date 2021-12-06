using BowlingGame.Application.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace BowlingGame.Infrastructure.Repositories.SQLServer
{
    public static class ConfigurationExtension
    {
        public static IServiceCollection RegisterSQLServerRepository(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if(configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                                                                                b => b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)));
            services.AddTransient<IGameRepository, GameRepository>();
            services.AddTransient<IScoreRepository, ScoreRepository>();

            return services;
        }
    }
}
