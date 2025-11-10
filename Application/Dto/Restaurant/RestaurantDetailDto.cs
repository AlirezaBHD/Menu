using System.ComponentModel.DataAnnotations;
using Domain.Common.Attributes;

namespace Application.Dto.Restaurant;

public class RestaurantDetailDto
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
    
    public Dictionary<string, string> OpeningHours { get; set; }
}