using Muno.Application.Dto.MenuItem;
using Muno.Application.Dto.Shared;
using Muno.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Muno.API.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class MenuItemsController(IMenuItemService menuItemService) : ControllerBase
{
    
    [SwaggerResponse(200, "menu-item created successfully", typeof(MenuItemResponse))]
    [HttpPost("/api/section/{sectionId:int}/[controller]")]
    public async Task<IActionResult> CreateMenuItem(int sectionId,
        [FromBody] CreateMenuItemRequest createMenuItemRequest)
    {
        var menuItem = await menuItemService.CreateMenuItemAsync(sectionId: sectionId,
            createMenuItemRequest: createMenuItemRequest);
        return Ok(menuItem);
    }
    
    
    [SwaggerResponse(200, "menu-item image changed successfully", typeof(string))]
    [HttpPatch("{id:int}/image")]
    public async Task<IActionResult> PatchMenuItemImage(int id,
        [FromForm] ImageDto image)
    {
        var path = await menuItemService.EditImageAsync(id, image);
        return Ok(path);
    }
    
    
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetMenuItemById(int id)
    {
        var menuItem = await menuItemService.GetMenuItemByIdAsync(id);
        return Ok(menuItem);
    }
    
    
    [SwaggerResponse(201, "menu-item deleted successfully")]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteMenuItemById(int id)
    {
        await menuItemService.DeleteMenuItemAsync(id: id);
        return NoContent();
    }
    
    
    [SwaggerResponse(201, "menu-item updated successfully")]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateMenuItem(int id,
        [FromBody] UpdateMenuItemRequest updateMenuItemRequest)
    {
        await menuItemService.UpdateMenuItemAsync(id: id, dto: updateMenuItemRequest);
        return NoContent();
    }
    
    
    [SwaggerResponse(200, "List of Sections", typeof(IEnumerable<MenuItemListResponse>))]
    [HttpGet]
    public async Task<IActionResult> GetMenuItems()
    {
        var categories = await menuItemService.GetMenuItemListAsync();
        return Ok(categories);
    }

    
    [SwaggerResponse(204, "MenuItem order updated successfully")]
    [HttpPatch("order")]
    public async Task<IActionResult> UpdateSectionOrder([FromBody] List<OrderDto> dto)
    {
        await menuItemService.UpdateMenuItemOrderAsync(dto);
        return NoContent();
    }
}