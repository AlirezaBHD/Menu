using System.ComponentModel.DataAnnotations;
using Muno.Domain.Common.Attributes;
using Muno.Application.Dto.MenuItem;

namespace Muno.Application.Dto.Section;

public class MenuSectionDto
{
    [Required]
    [MultiLanguageProperty]
    public string Title { get; set; }
    public List<ItemMenuDto> MenuItems { get; set; }
}