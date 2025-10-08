using Application.Dto.ActivityPeriod;
using Application.Dto.Section;
using Application.Dto.Shared;

namespace Application.Dto.Category;

public class CategoryResponse: IHasTranslationsDto<CategoryTranslationDto>
{
    public int Id { get; set; }
    public ICollection<CategoryTranslationDto> Translations { get; set; } = new List<CategoryTranslationDto>();
    public ActivityPeriodResponse? ActivityPeriod { get; set; }
    public List<SectionDto> Sections { get; set; } = [];
}