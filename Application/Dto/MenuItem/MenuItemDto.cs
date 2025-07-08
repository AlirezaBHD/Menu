using System.ComponentModel.DataAnnotations;

namespace Application.Dto.MenuItem;

public class MenuItemDto
{
    public Guid Id { get; set; }
    [Required]
    public string Title { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public string? ImagePath { get; set; }
}