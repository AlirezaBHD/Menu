using Muno.Application.Dto.ActivityPeriod;
using Muno.Application.Dto.MenuItemVariant;

namespace Muno.Application.Dto.MenuItem;

public class MenuItemDto
{
    public string? ImagePath { get; set; }
    public int Id { get; set; }
    public int SectionId { get; set; }
    public ICollection<MenuItemTranslationDto> Translations { get; set; } = new List<MenuItemTranslationDto>();
    public ActivityPeriodResponse ActivityPeriod { get; set; }
    public List<MenuItemVariantDto> Variants { get; set; }

}