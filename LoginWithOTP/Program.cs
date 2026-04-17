
using LoginWithOTP.DbLayer.Models;
using MongoDB.Driver;
namespace LoginWithOTP
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            // Mongo Client
            builder.Services.AddSingleton<IMongoClient>(sp =>
            {
                var settings = builder.Configuration
                    .GetSection("MongoDbSettings")
                    .Get<MongoDbSettings>();

                return new MongoClient(settings?.ConnectionString);
            });

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
