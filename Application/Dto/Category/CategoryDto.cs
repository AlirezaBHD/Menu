using System.ComponentModel.DataAnnotations;
using Application.Dto.Section;

namespace Application.Dto.Category;

public class CategoryDto
{
    [Required]
    public string Title { get; set; }
    public ICollection<AvailableMenuItemSectionDto> Sections { get; set; }
}