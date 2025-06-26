namespace Domain.Entities;

public class Section
{
    public Guid Id { get; set; }
    public string Title { get; set; } = default!;
    public Guid CategoryId { get; set; }

    public Category? Category { get; set; }
    public ICollection<MenuItem> MenuItems { get; set; } = new List<MenuItem>();
}
