using LoginWithOTP.Configurations;

namespace LoginWithOTP
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services
                .AddApplicationServices()
                .AddInfrastructureConfiguration(builder.Configuration)
                .AddJwtAuthentication(builder.Configuration)
                .AddSwaggerConfiguration();

            var app = builder.Build();
            await app.InitializeDatabaseAsync();
            app.UseAppMiddleware();
            app.UseSwaggerConfiguration();
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            await app.RunAsync();
        }
    }
}