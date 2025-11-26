using Muno.Domain.Interfaces.Services;
using Muno.Domain.Localization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;

namespace Muno.Application.Services;

public class CurrentLanguage(IHttpContextAccessor httpContextAccessor) : ICurrentLanguage
{
    private readonly string _defaultLanguage = SupportedLanguages.All[0].Code;

    public string GetLanguage()
    {
        var context = httpContextAccessor.HttpContext;
        if (context == null)
            return _defaultLanguage;

        var feature = context.Features.Get<IRequestCultureFeature>();
        return feature?.RequestCulture.UICulture.TwoLetterISOLanguageName ?? _defaultLanguage;
    }
}