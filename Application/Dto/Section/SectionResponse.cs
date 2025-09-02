using System.ComponentModel.DataAnnotations;
using Application.Dto.ActivityPeriod;
using Application.Dto.MenuItem;
using Domain.Common.Attributes;

namespace Application.Dto.Section;

public class SectionResponse
{
    public int Id { get; set; }
    [Required]
    [MultiLanguageProperty]
    public string Title { get; set; }
    public ActivityPeriodResponse ActivityPeriod { get; set; }
    public List<MenuItemResponse> MenuItems { get; set; }
}