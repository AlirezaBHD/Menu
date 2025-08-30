using Domain.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;

namespace Application.Services;

public class CurrentLanguage : ICurrentLanguage
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly string _defaultLanguage = "en-US";

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