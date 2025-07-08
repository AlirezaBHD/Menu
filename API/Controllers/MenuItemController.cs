using Application.Dto.Authentication;
using Application.Dto.Category;
using Application.Dto.MenuItem;
using Application.Services.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

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

    [SwaggerResponse(200, "Restaurant's all items", typeof(RestaurantMenuDto))]
    [HttpGet("/api/restaurant/{restaurantId}/[controller]")]
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
            UserName = "owner3",
            // UserName = "owner1",
            Email = "owner1@example.com",
            EmailConfirmed = true
        };

        var result = await _userManager.CreateAsync(user, "StrongPassword123!");

        if (result.Succeeded)
            return Ok(new { message = "User created successfully", userId = user.Id });

        return BadRequest(result.Errors);
    }
    
    [Authorize]
    [SwaggerResponse(200, "menu-item created successfully", typeof(MenuItemResponse))]
    [HttpPost("/api/section/{sectionId}/[controller]")]
    public async Task<IActionResult> CreateMenuItem([FromRoute] Guid sectionId,
        [FromForm] CreateMenuItemRequest createMenuItemRequest)
    {
        var menuItem = await _menuItemService.CreateMenuItemAsync(sectionId: sectionId,
            createMenuItemRequest: createMenuItemRequest);
        return Ok(menuItem);
    }
    
    [Authorize]
    [SwaggerResponse(201, "menu-item deleted successfully")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMenuItemById([FromRoute] Guid id)
    {
        await _menuItemService.DeleteMenuItemAsync(id: id);
        return NoContent();
    }
    
    [Authorize]
    [SwaggerResponse(201, "menu-item updated successfully")]
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateMenuItem([FromRoute] Guid id,
        [FromForm] UpdateMenuItemRequest updateMenuItemRequest)
    {
        await _menuItemService.UpdateMenuItemAsync(id: id, dto: updateMenuItemRequest);
        return NoContent();
    }
}