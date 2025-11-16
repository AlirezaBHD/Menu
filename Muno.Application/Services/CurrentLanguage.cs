using Domain.Interfaces.Services;
using Domain.Localization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;

namespace Muno.Application.Services;

public class CurrentLanguage : ICurrentLanguage
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly string _defaultLanguage = SupportedLanguages.All[0].Code;

    public CurrentLanguage(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string GetLanguage()
    {
        var context = _httpContextAccessor.HttpContext;
        if (context == null)
            return _defaultLanguage;

        var feature = context.Features.Get<IRequestCultureFeature>();
        return feature?.RequestCulture.UICulture.TwoLetterISOLanguageName ?? _defaultLanguage;
    }
}