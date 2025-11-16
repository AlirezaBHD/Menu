using Muno.Application.Dto.ActivityPeriod;
using Muno.Application.Dto.Shared;

namespace Muno.Application.Dto.Section;

public class CreateSectionRequest : IHasTranslationsDto<SectionTranslationDto>
{
    public ActivityPeriodRequest ActivityPeriod { get; set; }
    public ICollection<SectionTranslationDto> Translations { get; set; } = new List<SectionTranslationDto>();
}