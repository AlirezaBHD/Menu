using System.ComponentModel.DataAnnotations;

namespace Application.Dto.Section;

public class SectionListResponse
{
    public Guid Id { get; set; }
    [Required]
    public string Title { get; set; }
    public int Order { get; set; }
    public string CategoryTitle { get; set; }
}