using LoginWithOTP.DbLayer.DbContexts;
using LoginWithOTP.DbLayer.Initializers;
using LoginWithOTP.DbLayer.Models;
using LoginWithOTP.Shared.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using StackExchange.Redis;

namespace LoginWithOTP.Configurations
{
    public static class InfrastructureConfiguration
    {
        public static IServiceCollection AddInfrastructureConfiguration(
            this IServiceCollection services,
            IConfiguration config)
        {
            services.Configure<MongoDbSettings>(
                config.GetSection("MongoDbSettings"));

            services.Configure<RedisSettings>(
                config.GetSection("RedisSettings"));

            services.Configure<JwtSettings>(
                config.GetSection("JwtSettings"));

            services.AddSingleton(sp =>
                sp.GetRequiredService<IOptions<MongoDbSettings>>().Value);

            services.AddSingleton(sp =>
                sp.GetRequiredService<IOptions<RedisSettings>>().Value);

            services.AddSingleton(sp =>
                sp.GetRequiredService<IOptions<JwtSettings>>().Value);

            services.AddSingleton<IMongoClient>(sp =>
            {
                var settings = sp.GetRequiredService<MongoDbSettings>();
                return new MongoClient(settings.ConnectionString);
            });

            services.AddSingleton<MongoDbContext>();
            services.AddSingleton<IConnectionMultiplexer>(sp =>
            {
                var redis = sp.GetRequiredService<RedisSettings>();
                return ConnectionMultiplexer.Connect(redis.ConnectionString);
            });
            services.AddSingleton<MongoInitializer>();
            return services;
        }
    }
}