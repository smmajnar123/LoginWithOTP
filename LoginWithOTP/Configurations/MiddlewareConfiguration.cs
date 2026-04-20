using LoginWithOTP.Core.Middlewares;

namespace LoginWithOTP.Configurations
{
    public static class MiddlewareConfiguration
    {
        public static IApplicationBuilder UseAppMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<GlobalExceptionMiddleware>();
            return app;
        }
    }
}