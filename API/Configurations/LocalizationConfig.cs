using System.Globalization;
using Application.Exceptions;
using Domain.Interfaces.Services;
using Domain.Localization;
using Microsoft.AspNetCore.Localization;

namespace API.Configurations;

public static class LocalizationConfig
{
    public static IServiceCollection AddAppLocalization(this IServiceCollection services)
    {
        services.Configure<RequestLocalizationOptions>(options =>
        {
            var supportedCultures = SupportedLanguages.All.Select(l => l.Code).ToArray();

            options.DefaultRequestCulture = new RequestCulture(supportedCultures.First());
            options.SupportedCultures = supportedCultures.Select(c => new CultureInfo(c)).ToList();
            options.SupportedUICultures = supportedCultures.Select(c => new CultureInfo(c)).ToList();

            options.RequestCultureProviders.Insert(0, new CookieRequestCultureProvider());
            options.RequestCultureProviders.Insert(1, new QueryStringRequestCultureProvider());
        });

        return services;
    }

    public static IApplicationBuilder ConfigureApplicationLanguage(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var currentLang = scope.ServiceProvider.GetRequiredService<ICurrentLanguage>();
        MultiLanguageMappingExtensions.Configure(currentLang);

        return app;
    }
}