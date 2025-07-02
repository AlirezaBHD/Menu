namespace Application.Dto.Section;

public class UpdateSectionRequest
{
    public string Title { get; set; }
    public List<Guid> MenuItemIds { get; set; }
}