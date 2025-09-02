using Application.Dto.ActivityPeriod;
using Application.Dto.Shared;

namespace Application.Dto.Category;

public class CreateCategoryRequest : IHasTranslationsDto<CategoryTranslationDto>
{
    public ActivityPeriodRequest ActivityPeriod { get; set; }
    public ICollection<CategoryTranslationDto> Translations { get; set; } = new List<CategoryTranslationDto>();
}