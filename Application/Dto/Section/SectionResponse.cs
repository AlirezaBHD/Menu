using Application.Dto.MenuItem;

namespace Application.Dto.Section;

public class SectionResponse
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public List<MenuItemResponse> MenuItems { get; set; }
}