using Application.Dto.MenuItem;

namespace Application.Dto.Section;

public class SectionDto
{
    public string Title { get; set; }
    public List<MenuItemDto> MenuItems { get; set; }
}