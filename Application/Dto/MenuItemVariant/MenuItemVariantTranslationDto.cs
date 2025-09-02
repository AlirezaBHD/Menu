using Application.Dto.Shared;

namespace Application.Dto.MenuItemVariant;

public class MenuItemVariantTranslationDto : TranslationDto
{
    public string Detail { get; set; }  = "";

    public decimal Price { get; set; }
}