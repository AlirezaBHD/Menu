using System.ComponentModel.DataAnnotations;
using Application.Dto.ActivityPeriod;
using Application.Dto.MenuItemVariant;
using Domain.Common.Attributes;

namespace Application.Dto.MenuItem;

public class ItemMenuDto
{
    public int Id { get; set; }
    
    public string? ImagePath { get; set; }
    
    [Required]
    [MultiLanguageProperty]
    public string Title { get; set; }
    
    [Required]

    [MultiLanguageProperty] 
    public string? Description { get; set; }
    public ActivityPeriodResponse ActivityPeriod { get; set; }
    public List<MenuItemVariantResponseDto> Variants { get; set; }
}