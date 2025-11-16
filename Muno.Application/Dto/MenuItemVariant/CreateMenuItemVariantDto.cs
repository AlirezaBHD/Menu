namespace Muno.Application.Dto.MenuItemVariant;

public class CreateMenuItemVariantDto
{
    public bool IsAvailable { get; set; }
    public ICollection<MenuItemVariantTranslationDto> Translations { get; set; } = new List<MenuItemVariantTranslationDto>();
}