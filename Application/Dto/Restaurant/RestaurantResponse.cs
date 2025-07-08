using System.ComponentModel.DataAnnotations;

namespace Application.Dto.Restaurant;

public class RestaurantResponse
{
    [Required]
    public string Name { get; set; }
    [Required]
    public string Address { get; set; }
    public string? Description { get; set; }
    public string? LogoPath { get; set; }
    [Required]
    public Dictionary<string, string> OpeningHours { get; set; }
}