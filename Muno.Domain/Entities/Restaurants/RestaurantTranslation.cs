using System.ComponentModel.DataAnnotations;
using Muno.Domain.Common.Attributes;
using Muno.Domain.Localization;

namespace Muno.Domain.Entities.Restaurants;

public class RestaurantTranslation : BaseTranslationEntity<Restaurant>
{
    [LocalizeDisplay(nameof(Resources.Name))]
    [MaxLength(50)]
    public required string Name { get; set; }

    [LocalizeDisplay(nameof(Resources.Address))]
    
    [MaxLength(500)]
    public required string Address { get; set; }

    [LocalizeDisplay(nameof(Resources.Description))]
    [MaxLength(500)]
    public string? Description { get; set; }
}