using Application.Dto.ActivityPeriod;
using Application.Dto.Shared;

namespace Application.Dto.Section;

public class UpdateSectionRequest: IHasTranslationsDto<SectionTranslationDto>
{
    public ActivityPeriodRequest ActivityPeriod { get; set; }
    public ICollection<SectionTranslationDto> Translations { get; set; }
}