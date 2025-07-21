using System.ComponentModel.DataAnnotations;

namespace Application.Dto.MenuItem;

public class MenuItemResponse
{
    public Guid Id { get; set; }
    [Required]
    public string Title { get; set; }
}