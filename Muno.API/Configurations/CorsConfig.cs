namespace Muno.API.Configurations;

public static class CorsConfig
{
    public static IServiceCollection AddAppCors(this IServiceCollection services, IConfiguration configuration)
    {
        var frontServer = configuration["FrontEnd:Url"];

        services.AddCors(options =>
        {
            options.AddPolicy("AllowAll", policy =>
            {
                policy.AllowCredentials()
                    .WithOrigins(frontServer!)
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });
        });

        return services;
    }
}