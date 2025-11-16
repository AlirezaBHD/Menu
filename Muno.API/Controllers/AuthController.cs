using Muno.Application.Dto.Authentication;
using Muno.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Muno.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }


    [Authorize(Roles = "SuperAdmin, Moderator")]
    [SwaggerResponse(201, "admin created successfully")]
    [HttpPost("register")]
    public async Task<IActionResult> CreateAdmin(RegisterAdminRequest request)
    {
        await _authService.CreateAdminAsync(request);
        return NoContent();
    }
    
    [SwaggerResponse(200, "Successful Login", typeof(LoginResponse))]
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var token = await _authService.LoginAsync(request);
        return Ok(token);
    }
}
