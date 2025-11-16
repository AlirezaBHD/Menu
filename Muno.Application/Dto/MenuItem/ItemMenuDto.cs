using System.ComponentModel.DataAnnotations;
using Domain.Common.Attributes;
using Muno.Application.Dto.ActivityPeriod;
using Muno.Application.Dto.MenuItemVariant;

namespace Muno.Application.Dto.MenuItem;

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