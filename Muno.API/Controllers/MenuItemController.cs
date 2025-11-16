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

namespace Muno.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MenuItemController : ControllerBase
{
    private readonly IMenuItemService _menuItemService;


    public MenuItemController(IMenuItemService menuItemService)
    {
        _menuItemService = menuItemService;
    }
    
    [Authorize]
    [SwaggerResponse(200, "menu-item created successfully", typeof(MenuItemResponse))]
    [HttpPost("/api/section/{sectionId}/[controller]")]
    public async Task<IActionResult> CreateMenuItem([FromRoute] int sectionId,
        [FromBody] CreateMenuItemRequest createMenuItemRequest)
    {
        var menuItem = await _menuItemService.CreateMenuItemAsync(sectionId: sectionId,
            createMenuItemRequest: createMenuItemRequest);
        return Ok(menuItem);
    }
    
    [Authorize]
    [SwaggerResponse(200, "menu-item image changed successfully", typeof(string))]
    [HttpPatch("/api/[controller]/{id}/image")]
    public async Task<IActionResult> PatchMenuItemImage([FromRoute] int id,
        [FromForm] ImageDto image)
    {
        var path = await _menuItemService.EditImageAsync(id, image);
        return Ok(path);
    }
    
    [Authorize]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetMenuItemById([FromRoute] int id)
    {
        var menuItem = await _menuItemService.GetMenuItemByIdAsync(id);
        return Ok(menuItem);
    }
    
    
    [Authorize]
    [SwaggerResponse(201, "menu-item deleted successfully")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMenuItemById([FromRoute] int id)
    {
        await _menuItemService.DeleteMenuItemAsync(id: id);
        return NoContent();
    }
    
    [Authorize]
    [SwaggerResponse(201, "menu-item updated successfully")]
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateMenuItem([FromRoute] int id,
        [FromBody] UpdateMenuItemRequest updateMenuItemRequest)
    {
        await _menuItemService.UpdateMenuItemAsync(id: id, dto: updateMenuItemRequest);
        return NoContent();
    }
    
    [Authorize]
    [SwaggerResponse(200, "List of Sections", typeof(IEnumerable<MenuItemListResponse>))]
    [HttpGet]
    public async Task<IActionResult> GetMenuItems()
    {
        var categories = await _menuItemService.GetMenuItemListAsync();
        return Ok(categories);
    }

    [Authorize]
    [SwaggerResponse(204, "MenuItem order updated successfully")]
    [HttpPatch("order")]
    public async Task<IActionResult> UpdateSectionOrder([FromBody] List<OrderDto> dto)
    {
        await _menuItemService.UpdateMenuItemOrderAsync(dto);
        return NoContent();
    }
}