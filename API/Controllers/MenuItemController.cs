using Application.Dto.MenuItem;
using Application.Services.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MenuItemController : ControllerBase
{
    private readonly IMenuItemService _menuItemService;
    private readonly UserManager<ApplicationUser> _userManager;


    public MenuItemController(IMenuItemService menuItemService, UserManager<ApplicationUser> userManager)
    {
        _menuItemService = menuItemService;
        _userManager = userManager;
    }

    [HttpGet("/api/[controller]/restaurant/{restaurantId}")]
    public async Task<IActionResult> GetAll([FromRoute] Guid restaurantId)
    {
        var menus = await _menuItemService.GetRestaurantMenuAsync(restaurantId);
        return Ok(menus);
    }
    
    [HttpPost("create-test-user")]
    public async Task<IActionResult> CreateTestUser()
    {
        var user = new ApplicationUser
        {
            UserName = "owner1",
            Email = "owner1@example.com",
            EmailConfirmed = true
        };

        var result = await _userManager.CreateAsync(user, "StrongPassword123!");

        if (result.Succeeded)
            return Ok(new { message = "User created successfully", userId = user.Id });

        return BadRequest(result.Errors);
    }
    
    [HttpPost("/api/section/{sectionId}/[controller]")]
    public async Task<IActionResult> CreateMenuItem([FromRoute] Guid sectionId,
        [FromBody] CreateMenuItemRequest createMenuItemRequest)
    {
        var menuItem = await _menuItemService.CreateMenuItemAsync(sectionId: sectionId,
            createMenuItemRequest: createMenuItemRequest);
        return Ok(menuItem);
    }
    
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMenuItemById([FromRoute] Guid id)
    {
        await _menuItemService.DeleteMenuItemAsync(id: id);
        return NoContent();
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateMenuItem([FromRoute] Guid id,
        [FromBody] UpdateMenuItemRequest updateMenuItemRequest)
    {
        await _menuItemService.UpdateMenuItemAsync(id: id, dto: updateMenuItemRequest);
        return NoContent();
    }
}