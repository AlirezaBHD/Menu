using Domain.Entities;

namespace Domain.Localization;

public static class SupportedLanguages
{
    public static readonly List<Language> All =
    [
        new Language { Code = "en-US", IsRtl = false, DisplayName = "English" },
        new Language { Code = "fa-IR", IsRtl = true, DisplayName = "فارسی" }
    ];
}