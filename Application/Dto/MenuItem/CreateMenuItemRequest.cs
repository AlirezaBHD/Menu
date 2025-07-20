using Application.Dto.ActivityPeriod;
using Microsoft.AspNetCore.Http;

namespace Application.Dto.MenuItem;

public class CreateMenuItemRequest
{
    public string Title { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public IFormFile ImageFile { get; set; }
    public bool IsAvailable { get; set; }
    public ActivityPeriodRequest ActivityPeriod { get; set; }
}