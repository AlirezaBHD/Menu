using Application.Dto.Authentication;
using Application.Dto.Category;
using Application.Dto.MenuItem;
using Application.Dto.Shared;
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
    
    [Authorize]
    [SwaggerResponse(200, "List of Sections", typeof(IEnumerable<MenuItemListResponse>))]
    [HttpGet]
    public async Task<IActionResult> GetSections()
    {
        var categories = await _menuItemService.GetMenuItemListAsync();
        return Ok(categories);
    }

    [Authorize]
    [SwaggerResponse(204, "Section order updated successfully")]
    [HttpPatch("order")]
    public async Task<IActionResult> UpdateSectionOrder([FromBody] List<OrderDto> dto)
    {
        await _menuItemService.UpdateMenuItemOrderAsync(dto);
        return NoContent();
    }
}