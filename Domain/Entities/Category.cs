using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

[DisplayName("دسته بندی")]
public class Category: BaseEntity
{
    [MaxLength(50)]
    [Display(Name = "عنوان")]
    public required string Title { get; set; }
    public Guid RestaurantId { get; set; }

    public Restaurant? Restaurant { get; set; }
    public ICollection<Section> Sections { get; set; } = new List<Section>();
}
