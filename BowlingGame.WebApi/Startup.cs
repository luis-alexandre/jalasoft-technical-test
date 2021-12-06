using BowlingGame.Infrastructure.Repositories.MongoDb;
using BowlingGame.Infrastructure.Repositories.SQLServer;
using BowlingGame.Infrastructure.Service;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;

namespace BowlingGame.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "BowlingGame.WebApi", Version = "v1" });
            });

            services.AddMediatR(typeof(Application.UseCases.GetScore.GetScoreRequest), 
                                typeof(Application.UseCases.AddScore.AddScoreRequest),
                                typeof(Application.UseCases.CreateGame.CreateGameRequest));

            var useNoSQL = Configuration.GetValue<bool>("UseNoSQL");

            if (useNoSQL)
            {
                services.RegisterMongoDbRepository(Configuration);
            }
            else
            {
                services.RegisterSQLServerRepository(Configuration);
            }

            services.RegisterCacheService();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BowlingGame.WebApi v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
