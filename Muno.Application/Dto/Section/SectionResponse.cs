using System.ComponentModel.DataAnnotations;
using Muno.Domain.Common.Attributes;
using Muno.Application.Dto.ActivityPeriod;
using Muno.Application.Dto.MenuItem;

namespace Muno.Application.Dto.Section;

public class SectionResponse
{
    public int Id { get; set; }
    public ICollection<SectionTranslationDto> Translations { get; set; } = new List<SectionTranslationDto>();
    public ActivityPeriodResponse ActivityPeriod { get; set; }
    public List<MenuItemResponse> MenuItems { get; set; }
}