using Muno.Application.Dto.Category;
using Muno.Application.Dto.Shared;
using Muno.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Muno.API.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class CategoriesController(ICategoryService categoryService) : ControllerBase
{
    
    [SwaggerResponse(200, "Category created Successfully", typeof(CategoryResponse))]
    [HttpPost]
    public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryRequest createCategoryRequest)
    {
        var category = await categoryService.CreateCategoryAsync(createCategoryRequest: createCategoryRequest);
        return Ok(category);
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> GetCategoryById([FromRoute] int id)
    {
        var category = await categoryService.GetCategoryByIdAsync(categoryId: id);
        return Ok(category);
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCategoryById([FromRoute] int id)
    {
        await categoryService.DeleteCategoryAsync(id: id);
        return NoContent();
    }


    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCategory([FromRoute] int id,
        [FromBody] UpdateCategoryRequest updateCategoryDto)
    {
        await categoryService.UpdateCategoryAsync(id: id, dto: updateCategoryDto);
        return NoContent();
    }


    [SwaggerResponse(200, "List of Categories", typeof(IEnumerable<CategoryListResponse>))]
    [HttpGet]
    public async Task<IActionResult> GetCategories()
    {
        var categories = await categoryService.GetCategoryListAsync();
        return Ok(categories);
    }


    [SwaggerResponse(204, "Category order updated successfully")]
    [HttpPatch("order")]
    public async Task<IActionResult> UpdateCategoryOrder([FromBody] List<OrderDto> dto)
    {
        await categoryService.UpdateCategoryOrderAsync(dto);
        return NoContent();
    }
}