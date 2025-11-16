using Muno.Application.Dto.ActivityPeriod;
using Muno.Application.Dto.Shared;

namespace Muno.Application.Dto.Category;

public class UpdateCategoryRequest : IHasTranslationsDto<CategoryTranslationDto>
{
    public List<int> SectionIds { get; set; }
    public ActivityPeriodRequest ActivityPeriod { get; set; }
    public ICollection<CategoryTranslationDto> Translations { get; set; } = new List<CategoryTranslationDto>();
}