using Application.Dto.ActivityPeriod;
using Application.Dto.Shared;

namespace Application.Dto.Section;

public class CreateSectionRequest : IHasTranslationsDto<SectionTranslationDto>
{
    public ActivityPeriodRequest ActivityPeriod { get; set; }
    public ICollection<SectionTranslationDto> Translations { get; set; } = new List<SectionTranslationDto>();
}