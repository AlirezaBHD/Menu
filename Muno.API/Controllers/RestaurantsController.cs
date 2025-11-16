using Muno.Application.Dto.Restaurant;
using Muno.Application.Dto.Shared;
using Muno.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Muno.API.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class RestaurantsController(IRestaurantService restaurantService) : ControllerBase
{
    
    [AllowAnonymous]
    [SwaggerResponse(200, "Restaurant's all items", typeof(RestaurantMenuDto))]
    [HttpGet("{id:int}/menu-items")]
    public async Task<IActionResult> GetRestaurantMenuAsync(int id)
    {
        var menus = await restaurantService.GetRestaurantMenuAsync(id);
        return Ok(menus);
    }


    [SwaggerResponse(200, "Restaurant created successfully", typeof(ResponseDto))]
    [HttpPost]
    public async Task<IActionResult> CreateRestaurant([FromBody] CreateRestaurantRequest createRestaurantRequest)
    {
        var response = await restaurantService.CreateRestaurantAsync(createRestaurantRequest: createRestaurantRequest);
        return Ok(response);
    }


    [SwaggerResponse(200, "restaurant image changed successfully", typeof(string))]
    [HttpPatch("{id:int}/image")]
    public async Task<IActionResult> PatchRestaurantImage(int id, [FromForm] ImageDto image)
    {
        var path = await restaurantService.EditImageAsync(id, image);
        return Ok(path);
    }


    [SwaggerResponse(201, "Restaurant updated successfully")]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateRestaurant(int id,
        [FromBody] UpdateRestaurantRequest updateRestaurantRequest)
    {
        await restaurantService.UpdateRestaurantAsync(id: id, dto: updateRestaurantRequest);
        return NoContent();
    }


    [SwaggerResponse(200, "Restaurant's information", typeof(RestaurantResponse))]
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetRestaurantById(int id)
    {
        var result = await restaurantService.GetRestaurantByIdAsync(id: id);
        return Ok(result);
    }


    [SwaggerResponse(204, "Restaurant order updated successfully")]
    [HttpPatch("order")]
    public async Task<IActionResult> UpdateRestaurantOrder([FromBody] List<OrderDto> dto)
    {
        await restaurantService.UpdateRestaurantOrderAsync(dto);
        return NoContent();
    }


    [AllowAnonymous]
    [SwaggerResponse(200, "Restaurants detail list", typeof(List<RestaurantDto>))]
    [HttpGet]
    public async Task<IActionResult> GetRestaurantsDetailList()
    {
        var result = await restaurantService.RestaurantDetailList();
        return Ok(result);
    }


    [AllowAnonymous]
    [SwaggerResponse(200, "Restaurants detail list", typeof(RestaurantDetailDto))]
    [HttpGet("{id:int}/detail")]
    public async Task<IActionResult> GetRestaurantsDetail(int id)
    {
        var result = await restaurantService.RestaurantDetail(id);
        return Ok(result);
    }
}