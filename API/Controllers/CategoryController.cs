using Application.Dto.Category;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/Restaurant/{restaurantId}/[controller]")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;


    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateCategory([FromRoute] Guid restaurantId,
        [FromBody] CreateCategoryRequest createCategoryRequest)
    {
        var category = await _categoryService.CreateCategory(restaurantId: restaurantId,
            createCategoryRequest: createCategoryRequest);
        return Ok(category);
    }
}