using Muno.Application.Dto.User;
using Muno.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Muno.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }
    
    [Authorize]
    [SwaggerResponse(200, "User's restaurants", typeof(IEnumerable<UserRestaurantsDto>))]
    [HttpGet("restaurant")]
    public async Task<IActionResult> GetUsersRestaurants()
    {
        var result = await _userService.Restaurants();
        return Ok(result);
    }
    
    [Authorize]
    [SwaggerResponse(204, "Restaurant Id have been set in session")]
    [HttpPost("restaurant/{Id}")]
    public async Task<IActionResult> SetRestaurantIdInSession([FromRoute] int Id)
    {
        await _userService.SetRestaurantIdInSessionAsync(Id);
        return NoContent();
    }
}