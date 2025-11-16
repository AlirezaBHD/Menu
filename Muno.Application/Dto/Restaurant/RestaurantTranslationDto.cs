using Muno.Application.Dto.Shared;

namespace Muno.Application.Dto.Restaurant;

public class RestaurantTranslationDto: TranslationDto
{
    public string Name { get; set; }
    public string Address { get; set; }
    public string? Description { get; set; }
}