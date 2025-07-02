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

    [HttpPost("/api/restaurant/{restaurantId}/[controller]")]
    public async Task<IActionResult> CreateCategory([FromRoute] Guid restaurantId,
        [FromBody] CreateCategoryRequest createCategoryRequest)
    {
        var category = await _categoryService.CreateCategoryAsync(restaurantId: restaurantId,
            createCategoryRequest: createCategoryRequest);
        return Ok(category);
    }

    public async Task<IActionResult> GetCategoryById([FromRoute] Guid id)
    {
        var category = await _categoryService.GetCategoryByIdAsync(categoryId: id);
        return Ok(category);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCategoryById([FromRoute] Guid id)
    {
        await _categoryService.DeleteCategoryAsync(id: id);
        return NoContent();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCategory([FromRoute] Guid id,
        [FromBody] UpdateCategoryRequest updateCategoryDto)
    {
        await _categoryService.UpdateCategoryAsync(id: id, dto: updateCategoryDto);
        return NoContent();
    }


}