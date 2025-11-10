using Domain.Common.Attributes;

namespace Application.Dto.MenuItemVariant;

public class MenuItemVariantResponseDto
{
    [MultiLanguageProperty]
    public string Detail { get; set; }  = "";

    [MultiLanguageProperty]
    public string Price { get; set; } = "0";
}