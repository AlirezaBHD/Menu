using Microsoft.AspNetCore.Http;

namespace Application.Dto.Restaurant;

public class CreateRestaurantRequest
{
    public string Name { get; set; }
    public int OwnerId { get; set; }
    public string Address { get; set; }
    public string? Description { get; set; }
    public IFormFile LogoFile { get; set; }
    public Dictionary<string, string> OpeningHours { get; set; }
}