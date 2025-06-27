using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

[DisplayName("رستوران")]
public class Restaurant: BaseEntity
{
    [MaxLength(50)]
    public required string Name { get; set; }
    public Guid OwnerId { get; set; }
    [MaxLength(500)]
    public required string Address { get; set; }
    [MaxLength(500)]
    public string? Description { get; set; }
    public string? LogoPath { get; set; }
    public Dictionary<string, string> OpeningHours { get; set; } = new();
    public ApplicationUser? Owner { get; set; }
    public ICollection<Category> Categories { get; set; } = new List<Category>();
}
