namespace Application.Dto.Category;

public class UpdateCategoryRequest
{
    public string Title { get; set; }
    public List<Guid> SectionIds { get; set; }
}