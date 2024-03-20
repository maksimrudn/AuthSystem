using Microsoft.AspNetCore.Builder;
using AuthSystem.WebApp;

namespace AuthSystem.WebApp.Middleware
{

    public static class CustomMiddlewareExtensions
    {
        public static void ConfigureCustomMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
            //app.UseMiddleware<JwtMiddleware>();
        }
    }
}