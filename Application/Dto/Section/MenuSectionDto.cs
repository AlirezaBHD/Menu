using System.ComponentModel.DataAnnotations;
using Application.Dto.MenuItem;

namespace Application.Dto.Section;

public class MenuSectionDto
{
    [Required]
    public string Title { get; set; }
    public List<MenuItemDto> MenuItems { get; set; }
}