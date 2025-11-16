using Muno.Application.Dto.Shared;

namespace Muno.Application.Dto.MenuItemVariant;

public class MenuItemVariantTranslationDto : TranslationDto
{
    public string Detail { get; set; }  = "";

    public string Price { get; set; }  = "0";
}