using System.ComponentModel.DataAnnotations;
using Application.Dto.Section;

namespace Application.Dto.Category;

public class RestaurantMenuDto
{
    [Required]
    public string Title { get; set; }
    public ICollection<AvailableMenuItemSectionDto> Sections { get; set; }
}