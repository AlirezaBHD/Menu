using System.ComponentModel.DataAnnotations;
using Application.Dto.Section;

namespace Application.Dto.Category;

public class MenuCategoryDto
{
    [Required]
    public string Title { get; set; }
    public ICollection<MenuSectionDto> Sections { get; set; }
}