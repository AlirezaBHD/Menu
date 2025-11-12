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
        app.UseMiddleware<SecurityHeadersMiddleware>();


        return app;
    }
}   