using Muno.Application.Dto.ActivityPeriod;
using Muno.Application.Dto.Section;
using Muno.Application.Dto.Shared;

namespace Muno.Application.Dto.Category;

public class CategoryResponse: IHasTranslationsDto<CategoryTranslationDto>
{
    public int Id { get; set; }
    public ICollection<CategoryTranslationDto> Translations { get; set; } = new List<CategoryTranslationDto>();
    public ActivityPeriodResponse? ActivityPeriod { get; set; }
    public List<SectionDto> Sections { get; set; } = [];
}