using Application.Dto.ActivityPeriod;
using Application.Dto.Shared;

namespace Application.Dto.Category;

public class UpdateCategoryRequest : IHasTranslationsDto<CategoryTranslationDto>
{
    public List<int> SectionIds { get; set; }
    public ActivityPeriodRequest ActivityPeriod { get; set; }
    public ICollection<CategoryTranslationDto> Translations { get; set; } = new List<CategoryTranslationDto>();
}