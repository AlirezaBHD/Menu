using System.ComponentModel.DataAnnotations;
using Application.Dto.AvailabilityPeriod;

namespace Application.Dto.MenuItem;

public class MenuItemResponse
{
    public Guid Id { get; set; }
    [Required]
    public string Title { get; set; }
    public AvailabilityPeriodResponse AvailabilityPeriod { get; set; }
}