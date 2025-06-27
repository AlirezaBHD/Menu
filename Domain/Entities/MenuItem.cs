using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

[DisplayName("آیتم منو")]
public class MenuItem: BaseEntity
{
    [MaxLength(50)]
    public required string Title { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public string? ImagePath { get; set; }
    public bool IsAvailable { get; set; } = true;
    public Guid SectionId { get; set; }

    public Section? Section { get; set; }
}
