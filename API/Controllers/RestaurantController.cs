using Application.Dto.Category;
using Application.Dto.Restaurant;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RestaurantController : ControllerBase
{
    private readonly IRestaurantService _restaurantService;

    public RestaurantController(IRestaurantService restaurantService)
    {
        _restaurantService = restaurantService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateRestaurant([FromForm] CreateRestaurantRequest createRestaurantRequest)
    {
        await _restaurantService.CreateRestaurantAsync(createRestaurantRequest: createRestaurantRequest);
        return NoContent();
    }
}