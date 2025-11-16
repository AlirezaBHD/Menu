using System.ComponentModel;
using Muno.Domain.Entities.Restaurants;
using Muno.Domain.Entities.Sections;
using Muno.Domain.Interfaces.Specifications;

namespace Muno.Domain.Entities.Categories;

[DisplayName("دسته بندی")]
public class Category: BaseEntity, ITranslation<CategoryTranslation>
{
    public ActivityPeriod ActivityPeriod { get; set; } = new();
    public int Order { get; set; }
    public int RestaurantId { get; set; }

    public Restaurant? Restaurant { get; set; }
    public ICollection<Section> Sections { get; set; } = new List<Section>();
    
    public ICollection<CategoryTranslation> Translations { get; set; } = new List<CategoryTranslation>();
}
