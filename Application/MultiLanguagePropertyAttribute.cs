namespace Application;

[AttributeUsage(AttributeTargets.Property)]
public class MultiLanguagePropertyAttribute : Attribute
{
    public string? TranslationPropertyName { get; }
    public MultiLanguagePropertyAttribute(string? translationPropertyName = null)
    {
        TranslationPropertyName = translationPropertyName;
    }
}
