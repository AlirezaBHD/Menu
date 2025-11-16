using System.ComponentModel.DataAnnotations;
using Muno.Application.Dto.ActivityPeriod;

namespace Muno.Application.Dto.Restaurant;

public class RestaurantResponse
{
    public int Id { get; set; }
    public ICollection<RestaurantTranslationDto> Translations { get; set; } = new List<RestaurantTranslationDto>();
    public string? LogoPath { get; set; }
    [Required]
    public Dictionary<string, string> OpeningHours { get; set; }
    public ActivityPeriodResponse ActivityPeriod { get; set; }
}