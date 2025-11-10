using System.ComponentModel.DataAnnotations;
using Application.Dto.MenuItem;
using Domain.Common.Attributes;

namespace Application.Dto.Section;

public class MenuSectionDto
{
    [Required]
    [MultiLanguageProperty]
    public string Title { get; set; }
    public List<ItemMenuDto> MenuItems { get; set; }
}