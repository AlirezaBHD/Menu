using System.ComponentModel;
using Muno.Domain.Common.Attributes;
using Muno.Domain.Entities.Categories;
using Muno.Domain.Interfaces.Specifications;
using Muno.Domain.Localization;

namespace Muno.Domain.Entities.Restaurants;

[DisplayName("رستوران")]
public class Restaurant : BaseEntity, ITranslation<RestaurantTranslation>
{
    public int OwnerId { get; set; }

    [LocalizeDisplay(nameof(Resources.LogoPath))]
    public string? LogoPath { get; set; }

    [LocalizeDisplay(nameof(Resources.OpeningHours))]
    public Dictionary<string, string> OpeningHours { get; set; } = new();

    public User? Owner { get; set; }

    public int Order { get; set; }
    public ActivityPeriod ActivityPeriod { get; set; } = new();

    public ICollection<Category> Categories { get; set; } = new List<Category>();

    public ICollection<RestaurantTranslation> Translations { get; set; } = new List<RestaurantTranslation>();
}