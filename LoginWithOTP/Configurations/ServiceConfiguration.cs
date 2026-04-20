using LoginWithOTP.Repository.IRepository;
using LoginWithOTP.Repository.Repository;
using LoginWithOTP.Services.IServices;
using LoginWithOTP.Services.Services;

namespace LoginWithOTP.Configurations
{
    public static class ServiceConfiguration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddControllers();
            services.AddScoped<IOtpRepository, OtpRepository>();
            services.AddScoped<IOtpService, OtpService>();
            services.AddScoped<IJwtService, JwtService>();
            return services;
        }
    }
}
