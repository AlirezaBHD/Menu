using Muno.Application.Dto.Shared;

namespace Muno.Application.Dto.MenuItemVariant;

public class MenuItemVariantDto : IHasTranslationsDto<MenuItemVariantTranslationDto>
{
    public int Id { get; set; }
    public bool IsAvailable { get; set; }
    public ICollection<MenuItemVariantTranslationDto> Translations { get; set; } = new List<MenuItemVariantTranslationDto>();
}