using System.ComponentModel.DataAnnotations;
using Domain.Common.Attributes;

namespace Muno.Application.Dto.Restaurant;

public class RestaurantDto
{
    public int Id { get; set; }

    [Required]
    [MultiLanguageProperty]
    public  string Name { get; set; }
    
    [Required]
    public string LogoPath { get; set; }
    
    [Required]
    [MultiLanguageProperty]
    public  string Description { get; set; }
}