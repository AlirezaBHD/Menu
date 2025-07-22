using Application.Dto.User;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Controllers;

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
    [HttpGet("restaurants")]
    public async Task<IActionResult> GetUsersRestaurants()
    {
        var result = await _userService.Restaurants();
        return Ok(result);
    }
    
}