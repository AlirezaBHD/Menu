using System.ComponentModel.DataAnnotations;
using Muno.Domain.Common.Attributes;
using Muno.Application.Dto.Category;

namespace Muno.Application.Dto.Restaurant;

public class RestaurantMenuDto
{
    [Required]
    [MultiLanguageProperty]
    public  string Name { get; set; }
    [Required]
    public string LogoPath { get; set; }
    public ICollection<MenuCategoryDto> Categories { get; set; }
}