using System.ComponentModel;
using Domain.Entities.Restaurants;
using Domain.Entities.Sections;

namespace Domain.Entities.Categories;

[DisplayName("دسته بندی")]
public class Category: BaseEntity
{
    public ActivityPeriod ActivityPeriod { get; set; } = new();
    public int Order { get; set; }
    public int RestaurantId { get; set; }

    public Restaurant? Restaurant { get; set; }
    public ICollection<Section> Sections { get; set; } = new List<Section>();
    
    public ICollection<CategoryTranslation> Translations { get; set; } = new List<CategoryTranslation>();
}
