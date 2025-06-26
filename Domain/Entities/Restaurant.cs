namespace Domain.Entities;

public class Restaurant
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public Guid OwnerId { get; set; }
    public string Address { get; set; } = default!;
    public string? Description { get; set; }
    public string? LogoPath { get; set; }
    public Dictionary<string, string> OpeningHours { get; set; } = new();
    
    public ApplicationUser? Owner { get; set; }
    public ICollection<Category> Categories { get; set; } = new List<Category>();
}
