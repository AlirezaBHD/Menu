using System.ComponentModel.DataAnnotations;
using Application.Dto.AvailabilityPeriod;
using Application.Dto.MenuItem;

namespace Application.Dto.Section;

public class SectionResponse
{
    public Guid Id { get; set; }
    [Required]
    public string Title { get; set; }
    public AvailabilityPeriodResponse AvailabilityPeriod { get; set; }
    public List<MenuItemResponse> MenuItems { get; set; }
}