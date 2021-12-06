using BowlingGame.Application.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using System;

namespace BowlingGame.Infrastructure.Repositories.MongoDb
{
    public static class ConfigurationExtension
    {
        public static IServiceCollection RegisterMongoDbRepository(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            services.AddSingleton<IMongoClient>(new MongoClient(configuration["MongoDbSettings:CnnString"]));
            services.AddTransient(typeof(IMongoDbContext<>), typeof(MongDbContext<>));
            services.AddTransient<IGameRepository, GameRepository>();
            services.AddTransient<IScoreRepository, ScoreRepository>();

            return services;
        }
    }
}
