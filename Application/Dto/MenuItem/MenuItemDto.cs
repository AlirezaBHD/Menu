using System.ComponentModel.DataAnnotations;
using Application.Dto.MenuItemVariant;

namespace Application.Dto.MenuItem;

public class MenuItemDto
{
    [Required]
    [MultiLanguageProperty]
    public string Title { get; set; }
    [MultiLanguageProperty]
    public string? Description { get; set; }
    public string? ImagePath { get; set; }
    public bool IsAvailable { get; set; }
    public List<MenuItemVariantDto> Variants { get; set; }
}