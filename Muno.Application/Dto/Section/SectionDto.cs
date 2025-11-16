using System.ComponentModel.DataAnnotations;
using Domain.Common.Attributes;

namespace Muno.Application.Dto.Section;

public class SectionDto
{
    public int Id { get; set; }
    [Required]
    [MultiLanguageProperty]
    public string Title { get; set; }
}