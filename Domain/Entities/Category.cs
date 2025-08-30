using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

[DisplayName("دسته بندی")]
public class Category: BaseEntity
{
    [MaxLength(50)]
    [Display(Name = "عنوان")]
    public required string Title { get; set; }
    public ActivityPeriod ActivityPeriod { get; set; } = new();
    public int Order { get; set; }
    public int RestaurantId { get; set; }

    public Restaurant? Restaurant { get; set; }
    public ICollection<Section> Sections { get; set; } = new List<Section>();
}
