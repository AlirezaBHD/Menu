using System.ComponentModel.DataAnnotations;
using Muno.Domain.Common.Attributes;
using Muno.Application.Dto.Section;

namespace Muno.Application.Dto.Category;

public class MenuCategoryDto
{
    [Required]
    [MultiLanguageProperty]
    public string Title { get; set; }
    public ICollection<MenuSectionDto> Sections { get; set; }
}