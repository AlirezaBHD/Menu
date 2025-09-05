using System.ComponentModel;
using Domain.Common.Attributes;
using Domain.Entities.Categories;
using Domain.Interfaces;
using Domain.Localization;

namespace Domain.Entities.Restaurants;

[DisplayName("رستوران")]
public class Restaurant : BaseEntity, ITranslation<RestaurantTranslation>
{
    public int OwnerId { get; set; }
    
    [LocalizeDisplay(nameof(Resources.LogoPath))]
    public string? LogoPath { get; set; }

    [LocalizeDisplay(nameof(Resources.OpeningHours))]
    public Dictionary<string, string> OpeningHours { get; set; } = new();

    public User? Owner { get; set; }

    public ICollection<Category> Categories { get; set; } = new List<Category>();
    
    public ICollection<RestaurantTranslation> Translations { get; set; } = new List<RestaurantTranslation>();
}