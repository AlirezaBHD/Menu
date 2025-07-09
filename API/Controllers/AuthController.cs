using Application.Dto.Authentication;
using Application.Services.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IAuthService _authService;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public AuthController(UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager, IAuthService authService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _authService = authService;
    }


    [Authorize(Roles = "SuperAdmin, Moderator")]
    [SwaggerResponse(201, "admin created successfully")]
    [HttpPost]
    public async Task<IActionResult> CreateAdmin(RegisterAdminRequest request)
    {
        await _authService.CreateAdminAsync(request);
        return NoContent();
    }
    
    [SwaggerResponse(200, "Successful Login", typeof(LoginResponse))]
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var user = await _userManager.FindByNameAsync(request.Username);
        if (user == null)
            return Unauthorized("نام کاربری یا رمز عبور اشتباه است");

        var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
        if (!result.Succeeded)
            return Unauthorized("نام کاربری یا رمز عبور اشتباه است");
        
        // var token = GenerateJwtToken(user);
        var token = await _authService.LoginAsync(request);
        return Ok(token);
    }
}
