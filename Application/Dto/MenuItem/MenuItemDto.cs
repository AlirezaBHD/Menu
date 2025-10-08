using Application.Dto.ActivityPeriod;
using Application.Dto.MenuItemVariant;

namespace Application.Dto.MenuItem;

public class MenuItemDto
{
    public string? ImagePath { get; set; }
    public int Id { get; set; }
    public int SectionId { get; set; }
    public ICollection<MenuItemTranslationDto> Translations { get; set; } = new List<MenuItemTranslationDto>();
    public ActivityPeriodResponse ActivityPeriod { get; set; }
    public List<MenuItemVariantDto> Variants { get; set; }

}