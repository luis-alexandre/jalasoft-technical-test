using BowlingGame.Application.Service;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace BowlingGame.Infrastructure.Service
{
    public static class ConfigurationExtension
    {
        public static IServiceCollection RegisterCacheService(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.AddMemoryCache();
            services.AddSingleton(typeof(ICache<>), typeof(CacheInMemory<>));

            return services;
        }
    }
}
