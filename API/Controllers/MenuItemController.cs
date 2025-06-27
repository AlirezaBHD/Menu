using Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
namespace API.Controllers;

[ApiController]
[Route("api/Restaurant/{restaurantId}/[controller]")]
public class MenuItemController : ControllerBase
{
    private readonly IMenuItemService _menuItemService;

    public MenuItemController(IMenuItemService menuItemService)
    {
        _menuItemService = menuItemService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromRoute] Guid restaurantId)
    {
        var menus = await _menuItemService.GetMenuItemsAsync(restaurantId);
        return Ok(menus);
    }
}