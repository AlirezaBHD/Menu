using Application.Dto.Category;
using Application.Dto.Shared;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

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

    [Authorize]
    [SwaggerResponse(200, "Category created Successfully", typeof(CategoryResponse))]
    [HttpPost("[controller]")]
    public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryRequest createCategoryRequest)
    {
        var category = await _categoryService.CreateCategoryAsync(createCategoryRequest: createCategoryRequest);
        return Ok(category);
    }

    [Authorize]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetCategoryById([FromRoute] int id)
    {
        var category = await _categoryService.GetCategoryByIdAsync(categoryId: id);
        return Ok(category);
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCategoryById([FromRoute] int id)
    {
        await _categoryService.DeleteCategoryAsync(id: id);
        return NoContent();
    }

    [Authorize]
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCategory([FromRoute] int id,
        [FromBody] UpdateCategoryRequest updateCategoryDto)
    {
        await _categoryService.UpdateCategoryAsync(id: id, dto: updateCategoryDto);
        return NoContent();
    }

    [Authorize]
    [SwaggerResponse(200, "List of Categories", typeof(IEnumerable<CategoryListResponse>))]
    [HttpGet]
    public async Task<IActionResult> GetCategories()
    {
        var categories = await _categoryService.GetCategoryListAsync();
        return Ok(categories);
    }

    [Authorize]
    [SwaggerResponse(204, "Category order updated successfully")]
    [HttpPatch("order")]
    public async Task<IActionResult> UpdateCategoryOrder([FromBody] List<OrderDto> dto)
    {
        await _categoryService.UpdateCategoryOrderAsync(dto);
        return NoContent();
    }
}