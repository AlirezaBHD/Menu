namespace Application.Dto.Restaurant;

public class RestaurantResponse
{
    public string Name { get; set; }
    public string Address { get; set; }
    public string? Description { get; set; }
    public string? LogoPath { get; set; }
    public Dictionary<string, string> OpeningHours { get; set; }
}