using System.ComponentModel;

namespace Domain.Entities;

[DisplayName("بخش")]
public class Section: BaseEntity
{
    public required string Title { get; set; }
    public Guid CategoryId { get; set; }

    public Category? Category { get; set; }
    public ICollection<MenuItem> MenuItems { get; set; } = new List<MenuItem>();
}
