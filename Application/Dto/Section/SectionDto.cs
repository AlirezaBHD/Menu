using Application.Dto.MenuItem;

namespace Application.Dto.Section;

public class SectionDto
{
    public string Title { get; set; }
    public ICollection<MenuItemDto> MenuItems { get; set; } = new List<MenuItemDto>();
}