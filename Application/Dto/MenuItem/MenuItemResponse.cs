using System.ComponentModel.DataAnnotations;
using Application.Dto.ActivityPeriod;

namespace Application.Dto.MenuItem;

public class MenuItemResponse
{
    public Guid Id { get; set; }
    [Required]
    public string Title { get; set; }
    public ActivityPeriodResponse ActivityPeriod { get; set; }
}