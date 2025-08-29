using System.Collections;
using System.Reflection;
using AutoMapper;

namespace Application;

public static class MultiLanguageMappingExtensions
{
    public static IMappingExpression<TSource, TDestination> ForAllMultiLanguageMembers<TSource, TDestination>(
        this IMappingExpression<TSource, TDestination> map)
    {
        var destType = typeof(TDestination);

        var props = destType.GetProperties()
            .Where(p => p.GetCustomAttribute<MultiLanguagePropertyAttribute>() != null
                        && p.PropertyType == typeof(string));

        foreach (var prop in props)
        {
            var translationPropertyName = prop.GetCustomAttribute<MultiLanguagePropertyAttribute>()?.TranslationPropertyName ?? prop.Name;

            map.ForMember(prop.Name, opt =>
            {
                opt.MapFrom(src => GetFirstTranslationValue(src, translationPropertyName));
            });
        }

        return map;
    }

    private static object? GetFirstTranslationValue(object? src, string translationPropertyName)
    {
        if (src == null) return null;

        var translationsProp = src.GetType()
            .GetProperty("Translations", BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);

        if (translationsProp == null) return null;

        var translationsObj = translationsProp.GetValue(src) as IEnumerable;
        if (translationsObj == null) return null;

        var firstTranslation = translationsObj.Cast<object?>().FirstOrDefault();
        if (firstTranslation == null) return null;

        var translationProp = firstTranslation.GetType()
            .GetProperty(translationPropertyName, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);

        return translationProp?.GetValue(firstTranslation);
    }
}
