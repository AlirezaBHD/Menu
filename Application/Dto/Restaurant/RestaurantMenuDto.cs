using System.ComponentModel.DataAnnotations;
using Application.Dto.Category;
using Domain.Common.Attributes;

namespace Application.Dto.Restaurant;

public class RestaurantMenuDto
{
    [Required]
    [MultiLanguageProperty]
    public  string Name { get; set; }
    [Required]
    public string LogoPath { get; set; }
    public ICollection<MenuCategoryDto> Categories { get; set; }
}