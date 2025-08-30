using System.Collections;
using System.Reflection;
using AutoMapper;
using Domain.Interfaces.Services;

namespace Application;

public static class MultiLanguageMappingExtensions
{
    private static ICurrentLanguage? _currentLanguage;

    public static void Configure(ICurrentLanguage currentLanguage)
    {
        _currentLanguage = currentLanguage;
    }

    public static IMappingExpression<TSource, TDestination> ForAllMultiLanguageMembers<TSource, TDestination>(
        this IMappingExpression<TSource, TDestination> map)
    {
        var destType = typeof(TDestination);

        var props = destType.GetProperties()
            .Where(p => p.GetCustomAttribute<MultiLanguagePropertyAttribute>() != null
                        && p.PropertyType == typeof(string));

        foreach (var prop in props)
        {
            var translationPropertyName = prop
                .GetCustomAttribute<MultiLanguagePropertyAttribute>()?
                .TranslationPropertyName ?? prop.Name;

            map.ForMember(prop.Name, opt =>
            {
                opt.MapFrom(src => GetTranslationValue(src, translationPropertyName));
            });
        }

        return map;
    }

    private static object? GetTranslationValue(object? src, string translationPropertyName)
    {
        if (src == null || _currentLanguage == null) return null;

        var translationsProp = src.GetType()
            .GetProperty("Translations", BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);

        if (translationsProp == null) return null;

        var translationsObj = translationsProp.GetValue(src) as IEnumerable;
        if (translationsObj == null) return null;

        var lang = _currentLanguage.GetLanguage();
        
        var match = translationsObj.Cast<object?>()
            .FirstOrDefault(t =>
            {
                var codeProp = t?.GetType().GetProperty("LanguageCode", BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
                var code = codeProp?.GetValue(t)?.ToString();
                return code != null && code.StartsWith(lang, StringComparison.OrdinalIgnoreCase);
            });

        if (match != null)
        {
            var translationProp = match.GetType().GetProperty(translationPropertyName,
                BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
            return translationProp?.GetValue(match);
        }

        var firstTranslation = translationsObj.Cast<object?>().FirstOrDefault();
        if (firstTranslation != null)
        {
            var translationProp = firstTranslation.GetType().GetProperty(translationPropertyName,
                BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
            return translationProp?.GetValue(firstTranslation);
        }

        return null;
    }
}
