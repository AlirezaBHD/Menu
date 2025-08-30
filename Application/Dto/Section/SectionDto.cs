using System.ComponentModel.DataAnnotations;

namespace Application.Dto.Section;

public class SectionDto
{
    public int Id { get; set; }
    [Required]
    public string Title { get; set; }
}