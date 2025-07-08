using Application.Dto.Category;
using Application.Dto.Restaurant;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
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

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> CreateRestaurant([FromForm] CreateRestaurantRequest createRestaurantRequest)
    {
        await _restaurantService.CreateRestaurantAsync(createRestaurantRequest: createRestaurantRequest);
        return NoContent();
    }


    [Authorize]
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateRestaurant([FromRoute] Guid id,
        [FromForm] UpdateRestaurantRequest updateRestaurantRequest)
    {
        await _restaurantService.UpdateRestaurantAsync(id: id, dto: updateRestaurantRequest);
        return NoContent();
    }
    
    [Authorize]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetRestaurantById([FromRoute] Guid id)
    {
        var result = await _restaurantService.GetRestaurantByIdAsync(id: id);
        return Ok(result);
    }
}