using System.ComponentModel.DataAnnotations;
using Application.Dto.Category;

namespace Application.Dto.Restaurant;

public class RestaurantMenuDto
{
    [Required]
    public  string Name { get; set; }
    [Required]
    public string LogoPath { get; set; }
    public ICollection<CategoryDto> Categories { get; set; }
}