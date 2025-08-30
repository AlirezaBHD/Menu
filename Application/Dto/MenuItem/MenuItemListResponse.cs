using System.ComponentModel.DataAnnotations;

namespace Application.Dto.MenuItem;

public class MenuItemListResponse
{
    public int Id { get; set; }
    [Required]
    public string Title { get; set; }
    public int Order { get; set; }
    public string SectionTitle { get; set; }
}