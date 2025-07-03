using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

[DisplayName("بخش")]
public class Section: BaseEntity
{
    [MaxLength(50)]
    [Display(Name = "عنوان")]
    public required string Title { get; set; }
    public Guid? CategoryId { get; set; }

    public Category? Category { get; set; }
    public ICollection<MenuItem> MenuItems { get; set; } = new List<MenuItem>();
}
