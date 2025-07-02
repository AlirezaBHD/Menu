using Application.Dto.Section;

namespace Application.Dto.Category;

public class CategoryResponse
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public List<SectionDto> Sections { get; set; }
}