using System.ComponentModel.DataAnnotations;

namespace Application.Dto.Section;

public class SectionDto
{
    public Guid Id { get; set; }
    [Required]
    public string Title { get; set; }
}