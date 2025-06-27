using System.ComponentModel;

namespace Domain.Entities;

[DisplayName("دسته بندی")]
public class Category: BaseEntity
{
    public Guid Id { get; set; }
    public string Title { get; set; } = default!;
    public Guid RestaurantId { get; set; }

    public Restaurant? Restaurant { get; set; }
    public ICollection<Section> Sections { get; set; } = new List<Section>();
}
