using LoginWithOTP.DbLayer.DbContexts;
using LoginWithOTP.DbLayer.Initializers;
using LoginWithOTP.DbLayer.Models;
using LoginWithOTP.Shared.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace LoginWithOTP
{
    public class Program
    {
        public static async Task Main(string[] args)   // ✅ async Main
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.Configure<MongoDbSettings>(
                builder.Configuration.GetSection("MongoDbSettings"));

            builder.Services.Configure<JwtSettings>(
                builder.Configuration.GetSection("JwtSettings"));
            builder.Services.AddSingleton(sp =>
                sp.GetRequiredService<IOptions<MongoDbSettings>>().Value);

            builder.Services.AddSingleton(sp =>
                sp.GetRequiredService<IOptions<JwtSettings>>().Value);

            builder.Services.AddSingleton<IMongoClient>(sp =>
            {
                var settings = sp.GetRequiredService<MongoDbSettings>();
                return new MongoClient(settings.ConnectionString);
            });

            builder.Services.AddSingleton<MongoDbContext>();
            builder.Services.AddSingleton<MongoInitializer>();

            builder.Services.AddScoped<Repository.IRepository.IOtpRepository, Repository.Repository.OtpRepository>();
            //builder.Services.AddScoped<IUserRepository, UserRepository>();

           
            //builder.Services.AddScoped<Services.IServices.IOtpService, Services.Services.OtpService>();
            builder.Services.AddScoped<Services.IServices.IJwtService, Services.Services.JwtService>();

            // =========================
            // 🔹 Controllers & Swagger
            // =========================
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var initializer = scope.ServiceProvider.GetRequiredService<MongoInitializer>();
                await initializer.InitializeAsync();   
            }

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            await app.RunAsync();
        }
    }
}