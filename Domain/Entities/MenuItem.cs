namespace Domain.Entities;

public class MenuItem
{
    public Guid Id { get; set; }
    public string Title { get; set; } = default!;
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public string? ImagePath { get; set; }
    public bool IsAvailable { get; set; } = true;
    public Guid SectionId { get; set; }

    public Section? Section { get; set; }
}
