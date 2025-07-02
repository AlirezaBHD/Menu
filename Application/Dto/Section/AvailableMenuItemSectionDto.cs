using Application.Dto.MenuItem;

namespace Application.Dto.Section;

public class AvailableMenuItemSectionDto
{
    public string Title { get; set; }
    public List<MenuItemDto> MenuItems { get; set; }
}