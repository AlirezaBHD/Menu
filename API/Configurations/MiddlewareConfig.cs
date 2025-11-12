using API.Middlewares;

namespace API.Configurations;

public static class MiddlewareConfig
{
    public static IApplicationBuilder UseAppMiddlewares(this IApplicationBuilder app)
    {
        app.UseSession();
        app.UseRequestLocalization();
        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseCors("AllowAll");
        app.UseRateLimiter();

        app.UseMiddleware<UserIdEnricherMiddleware>();
        app.UseMiddleware<ExceptionHandlingMiddleware>();

        app.Use(async (context, next) =>
        {
            context.Response.Headers.Append("X-Content-Type-Options", "nosniff");
            context.Response.Headers.Append("X-Frame-Options", "DENY");
            context.Response.Headers.Append("X-XSS-Protection", "1; mode=block");
            await next();
        });

        return app;
    }
}   