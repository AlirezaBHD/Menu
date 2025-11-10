using Application.Dto.Shared;

namespace Application.Dto.MenuItemVariant;

public class MenuItemVariantTranslationDto : TranslationDto
{
    public string Detail { get; set; }  = "";

    public string Price { get; set; }  = "0";
}