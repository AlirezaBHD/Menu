using Muno.Application.Dto.User;
using Muno.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Muno.API.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class UsersController(IUserService userService) : ControllerBase
{

    [SwaggerResponse(200, "User's restaurants", typeof(IEnumerable<UserRestaurantsDto>))]
    [HttpGet("restaurant")]
    public async Task<IActionResult> GetUsersRestaurants()
    {
        var result = await userService.Restaurants();
        return Ok(result);
    }
    
    [SwaggerResponse(204, "Restaurant ID have been set in session")]
    [HttpPost("restaurant/{id:int}")]
    public async Task<IActionResult> SetRestaurantIdInSession([FromRoute] int id)
    {
        await userService.SetRestaurantIdInSessionAsync(id);
        return NoContent();
    }
}