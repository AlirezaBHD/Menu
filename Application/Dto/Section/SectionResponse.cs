using System.ComponentModel.DataAnnotations;
using Application.Dto.ActivityPeriod;
using Application.Dto.MenuItem;
using Domain.Common.Attributes;

namespace Application.Dto.Section;

public class SectionResponse
{
    public int Id { get; set; }
    public ICollection<SectionTranslationDto> Translations { get; set; } = new List<SectionTranslationDto>();
    public ActivityPeriodResponse ActivityPeriod { get; set; }
    public List<MenuItemResponse> MenuItems { get; set; }
}