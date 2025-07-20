using Application.Dto.AvailabilityPeriod;
using Microsoft.AspNetCore.Http;

namespace Application.Dto.MenuItem;

public class CreateMenuItemRequest
{
    public string Title { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public IFormFile ImageFile { get; set; }
    public AvailabilityPeriodRequest AvailabilityPeriod { get; set; }
}