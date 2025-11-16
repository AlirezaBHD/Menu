using Muno.Domain.Common.Attributes;
using Muno.Application.Dto.Shared;

namespace Muno.Application.Dto.Category;

public class CategoryTranslationDto : TranslationDto
{
    [MultiLanguageProperty]
    public string Title { get; set; }
}