using Muno.Application.Dto.ActivityPeriod;
using Muno.Application.Dto.Shared;

namespace Muno.Application.Dto.Category;

public class CreateCategoryRequest : IHasTranslationsDto<CategoryTranslationDto>
{
    public ActivityPeriodRequest ActivityPeriod { get; set; }
    public ICollection<CategoryTranslationDto> Translations { get; set; } = new List<CategoryTranslationDto>();
}