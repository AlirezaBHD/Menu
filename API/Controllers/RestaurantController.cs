using Application.Dto.Restaurant;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RestaurantController : ControllerBase
{
    private readonly IRestaurantService _restaurantService;

    public RestaurantController(IRestaurantService restaurantService, IMenuItemService menuItemService)
    {
        _restaurantService = restaurantService;
    }

    [SwaggerResponse(200, "Restaurant's all items", typeof(RestaurantMenuDto))]
    [HttpGet("{restaurantId}/menu-items")]
    public async Task<IActionResult> GetRestaurantMenuAsync([FromRoute] Guid restaurantId)
    {
        var menus = await _restaurantService.GetRestaurantMenuAsync(restaurantId);
        return Ok(menus);
    }
    
    [Authorize]
    [SwaggerResponse(201, "Restaurant created successfully")]
    [HttpPost]
    public async Task<IActionResult> CreateRestaurant([FromForm] CreateRestaurantRequest createRestaurantRequest)
    {
        await _restaurantService.CreateRestaurantAsync(createRestaurantRequest: createRestaurantRequest);
        return NoContent();
    }


    [Authorize]
    [SwaggerResponse(201, "Restaurant updated successfully")]
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateRestaurant([FromRoute] Guid id,
        [FromForm] UpdateRestaurantRequest updateRestaurantRequest)
    {
        await _restaurantService.UpdateRestaurantAsync(id: id, dto: updateRestaurantRequest);
        return NoContent();
    }
    
    [Authorize]
    [SwaggerResponse(200, "Restaurant's information", typeof(RestaurantResponse))]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetRestaurantById([FromRoute] Guid id)
    {
        var result = await _restaurantService.GetRestaurantByIdAsync(id: id);
        return Ok(result);
    }
}