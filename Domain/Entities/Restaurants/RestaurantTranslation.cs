using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.Restaurants;

public class RestaurantTranslation : BaseEntity
{
    [Display(Name = "نام")]
    [MaxLength(50)]
    public required string Name { get; set; }

    [Display(Name = "نشانی")]
    [MaxLength(500)]
    public required string Address { get; set; }

    [Display(Name = "توضیحات")]
    [MaxLength(500)]
    public string? Description { get; set; }

    public int RestaurantId { get; set; }
    public Restaurant Restaurant { get; set; } = default!;

    [MaxLength(10)] public string LanguageCode { get; set; } = default!;
    public Language Language { get; set; } = default!;
}