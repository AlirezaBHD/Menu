namespace Muno.Domain.Common.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public class MultiLanguagePropertyAttribute(string? translationPropertyName = null) : Attribute
{
    public string? TranslationPropertyName { get; } = translationPropertyName;
}
