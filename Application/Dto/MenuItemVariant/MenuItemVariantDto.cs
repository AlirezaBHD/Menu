using Application.Dto.Shared;

namespace Application.Dto.MenuItemVariant;

public class MenuItemVariantDto : IHasTranslationsDto<MenuItemVariantTranslationDto>
{
    public int Id { get; set; }
    public bool IsAvailable { get; set; }
    public ICollection<MenuItemVariantTranslationDto> Translations { get; set; } = new List<MenuItemVariantTranslationDto>();
}