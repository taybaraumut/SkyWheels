using SkyWheels.Contact.API.Middlewares;

namespace SkyWheels.Contact.API.Extensions.MiddlewareExtensions
{
    public static class MiddlewareExtension
    {
        public static IApplicationBuilder UseMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ContactValidationMiddleware>();
            app.UseMiddleware<GlobalErrorHandlingMiddleware>();

            return app;
        }
    }
}
