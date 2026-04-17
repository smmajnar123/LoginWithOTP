using LoginWithOTP.DbLayer.DbContexts;
using LoginWithOTP.DbLayer.Initializers;
using LoginWithOTP.DbLayer.Models;
using LoginWithOTP.Services.IServices;
using LoginWithOTP.Services.Services;
using LoginWithOTP.Shared.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using StackExchange.Redis;

namespace LoginWithOTP
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // =========================
            // 🔹 Configuration Binding
            // =========================
            builder.Services.Configure<MongoDbSettings>(
                builder.Configuration.GetSection("MongoDbSettings"));

            builder.Services.Configure<RedisSettings>(
                builder.Configuration.GetSection("RedisSettings"));

            builder.Services.Configure<JwtSettings>(
                builder.Configuration.GetSection("JwtSettings"));

            // =========================
            // 🔹 Strongly Typed Settings
            // =========================
            builder.Services.AddSingleton(sp =>
                sp.GetRequiredService<IOptions<MongoDbSettings>>().Value);

            builder.Services.AddSingleton(sp =>
                sp.GetRequiredService<IOptions<RedisSettings>>().Value);

            builder.Services.AddSingleton(sp =>
                sp.GetRequiredService<IOptions<JwtSettings>>().Value);

            // =========================
            // 🔹 MongoDB Setup
            // =========================
            builder.Services.AddSingleton<IMongoClient>(sp =>
            {
                var settings = sp.GetRequiredService<MongoDbSettings>();
                return new MongoClient(settings.ConnectionString);
            });

            builder.Services.AddSingleton<MongoDbContext>();

            // =========================
            // 🔹 Redis Setup
            // =========================
            builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
            {
                var redisSettings = sp.GetRequiredService<RedisSettings>();
                return ConnectionMultiplexer.Connect(redisSettings.ConnectionString);
            });

            // =========================
            // 🔹 Initializer
            // =========================
            builder.Services.AddSingleton<MongoInitializer>();

            // =========================
            // 🔹 Repositories / Stores
            // =========================
            builder.Services.AddScoped<Repository.IRepository.IOtpRepository, Repository.Repository.OtpRepository>();

            // =========================
            // 🔹 Services
            // =========================
            builder.Services.AddScoped<IOtpService, OtpService>();
            builder.Services.AddScoped<IJwtService, JwtService>();

            // =========================
            // 🔹 Controllers & Swagger
            // =========================
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // =========================
            // 🔥 Mongo Initialization
            // =========================
            using (var scope = app.Services.CreateScope())
            {
                var initializer = scope.ServiceProvider.GetRequiredService<MongoInitializer>();
                await initializer.InitializeAsync();
            }

            // =========================
            // 🔹 Middleware
            // =========================
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            // (JWT middleware can be added next)
            app.UseAuthorization();

            app.MapControllers();

            await app.RunAsync();
        }
    }
}