using Application.Dto.Section;

namespace Application.Dto.Category;

public class CategoryDto
{
    public string Title { get; set; }
    public ICollection<SectionDto> Sections { get; set; } = new List<SectionDto>();
}