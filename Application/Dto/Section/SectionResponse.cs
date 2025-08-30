using System.ComponentModel.DataAnnotations;
using Application.Dto.ActivityPeriod;
using Application.Dto.MenuItem;

namespace Application.Dto.Section;

public class SectionResponse
{
    public int Id { get; set; }
    [Required]
    public string Title { get; set; }
    public ActivityPeriodResponse ActivityPeriod { get; set; }
    public List<MenuItemResponse> MenuItems { get; set; }
}