using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Domain.Entities.Categories;

namespace Domain.Entities.Restaurants;

[DisplayName("رستوران")]
public class Restaurant : BaseEntity
{
    public int OwnerId { get; set; }
    [Display(Name = "لوگو")] public string? LogoPath { get; set; }

    [Display(Name = "ساعت های کاری مجموعه")]
    public Dictionary<string, string> OpeningHours { get; set; } = new();

    public User? Owner { get; set; }

    public ICollection<Category> Categories { get; set; } = new List<Category>();
    
    public ICollection<RestaurantTranslation> Translations { get; set; } = new List<RestaurantTranslation>();
}