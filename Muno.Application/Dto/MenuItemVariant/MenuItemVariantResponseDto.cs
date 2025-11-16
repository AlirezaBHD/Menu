using Domain.Common.Attributes;

namespace Muno.Application.Dto.MenuItemVariant;

public class MenuItemVariantResponseDto
{
    [MultiLanguageProperty]
    public string Detail { get; set; }  = "";

    [MultiLanguageProperty]
    public string Price { get; set; } = "0";
}