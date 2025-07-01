using Application.Dto.Category;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;


    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpPost("/Restaurant/{restaurantId}/[controller]")]
    public async Task<IActionResult> CreateCategory([FromRoute] Guid restaurantId,
        [FromBody] CreateCategoryRequest createCategoryRequest)
    {
        var category = await _categoryService.CreateCategory(restaurantId: restaurantId,
            createCategoryRequest: createCategoryRequest);
        return Ok(category);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCategoryById([FromRoute] Guid restaurantId, Guid id)
    {
        var category = await _categoryService.GetCategoryById(categoryId: id);
        return Ok(category);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteCategoryById(Guid id)
    {
        await _categoryService.DeleteCategoryAsync(id: id);
        return NoContent();
    }

}