using System.ComponentModel.DataAnnotations;
using Application.Dto.Section;
using Domain.Common.Attributes;

namespace Application.Dto.Category;

public class MenuCategoryDto
{
    [Required]
    [MultiLanguageProperty]
    public string Title { get; set; }
    public ICollection<MenuSectionDto> Sections { get; set; }
}