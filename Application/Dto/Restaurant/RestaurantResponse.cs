using System.ComponentModel.DataAnnotations;
using Domain.Common.Attributes;

namespace Application.Dto.Restaurant;

public class RestaurantResponse
{
    public int Id { get; set; }
    [Required]
    [MultiLanguageProperty]
    public string Name { get; set; }
    [Required]
    [MultiLanguageProperty]
    public string Address { get; set; }
    [MultiLanguageProperty]
    public string? Description { get; set; }
    public string? LogoPath { get; set; }
    [Required]
    public Dictionary<string, string> OpeningHours { get; set; }
}