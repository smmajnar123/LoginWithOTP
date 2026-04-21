using LoginWithOTP.DbLayer.Initializers;

namespace LoginWithOTP.Configurations
{
    public static class DbConfiguration
    {
        public static async Task InitializeDatabaseAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var initializer = scope.ServiceProvider.GetRequiredService<MongoInitializer>();
            await initializer.InitializeAsync();
        }
    }
}
