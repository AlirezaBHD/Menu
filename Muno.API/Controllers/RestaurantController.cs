using Muno.Application.Dto.Restaurant;
using Muno.Application.Dto.Shared;
using Muno.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Muno.API.Controllers;

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
    public async Task<IActionResult> GetRestaurantMenuAsync([FromRoute] int restaurantId)
    {
        var menus = await _restaurantService.GetRestaurantMenuAsync(restaurantId);
        return Ok(menus);
    }
    
    [Authorize]
    [SwaggerResponse(200, "Restaurant created successfully", typeof(ResponseDto))]
    [HttpPost]
    public async Task<IActionResult> CreateRestaurant([FromBody] CreateRestaurantRequest createRestaurantRequest)
    {
        var response = await _restaurantService.CreateRestaurantAsync(createRestaurantRequest: createRestaurantRequest);
        return Ok(response);
    }    
    
    
    [Authorize]
    [SwaggerResponse(200, "restaurant image changed successfully", typeof(string))]
    [HttpPatch("{id}/image")]
    public async Task<IActionResult> PatchRestaurantImage([FromRoute] int id,
        [FromForm] ImageDto image)
    {
        var path = await _restaurantService.EditImageAsync(id, image);
        return Ok(path);
    }


    [Authorize]
    [SwaggerResponse(201, "Restaurant updated successfully")]
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateRestaurant([FromRoute] int id,
        [FromBody] UpdateRestaurantRequest updateRestaurantRequest)
    {
        await _restaurantService.UpdateRestaurantAsync(id: id, dto: updateRestaurantRequest);
        return NoContent();
    }
    
    [Authorize]
    [SwaggerResponse(200, "Restaurant's information", typeof(RestaurantResponse))]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetRestaurantById([FromRoute] int id)
    {
        var result = await _restaurantService.GetRestaurantByIdAsync(id: id);
        return Ok(result);
    }
    
    [Authorize]
    [SwaggerResponse(204, "Restaurant order updated successfully")]
    [HttpPatch("order")]
    public async Task<IActionResult> UpdateRestaurantOrder([FromBody] List<OrderDto> dto)
    {
        await _restaurantService.UpdateRestaurantOrderAsync(dto);
        return NoContent();
    }
    
    [SwaggerResponse(200, "Restaurants detail list", typeof(List<RestaurantDto>))]
    [HttpGet]
    public async Task<IActionResult> GetRestaurantsDetailList()
    {
        var result =await _restaurantService.RestaurantDetailList();
        return Ok(result);
    }
    
    [SwaggerResponse(200, "Restaurants detail list", typeof(RestaurantDetailDto))]
    [HttpGet("{id}/detail")]
    public async Task<IActionResult> GetRestaurantsDetail([FromRoute] int id)
    {
        RestaurantDetailDto result = await _restaurantService.RestaurantDetail(id);
        return Ok(result);
    }
}