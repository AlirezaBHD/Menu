using Application.Dto.Shared;
using Domain.Common.Attributes;

namespace Application.Dto.Category;

public class CategoryTranslationDto : TranslationDto
{
    [MultiLanguageProperty]
    public string Title { get; set; }
}