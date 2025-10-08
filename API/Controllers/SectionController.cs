using Application.Dto.Section;
using Application.Dto.Shared;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SectionController : ControllerBase
{
    private readonly ISectionService _sectionService;

    public SectionController(ISectionService sectionService)
    {
        _sectionService = sectionService;
    }

    [Authorize]
    [SwaggerResponse(200, "Section created successfully", typeof(SectionResponse))]
    [HttpPost("/api/category/{categoryId}/[controller]")]
    public async Task<IActionResult> CreateSection([FromRoute] int categoryId,
        [FromBody] CreateSectionRequest createSectionRequest)
    {
        var section = await _sectionService.CreateSectionAsync(categoryId: categoryId,
            createSectionRequest: createSectionRequest);
        return Ok(section);
    }
    
    [Authorize]
    [SwaggerResponse(200, "Section information", typeof(SectionResponse))]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetSectionById([FromRoute] int id)
    {
        var section = await _sectionService.GetSectionByIdAsync(sectionId: id);
        return Ok(section);
    }
    
    [Authorize]
    [SwaggerResponse(201, "Section deleted successfully")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSectionById([FromRoute] int id)
    {
        await _sectionService.DeleteSectionAsync(id: id);
        return NoContent();
    }
    
    [Authorize]
    [SwaggerResponse(201, "Section updated successfully")]
    [HttpPut("/api/category/{categoryId}/[controller]/{id}")]
    public async Task<IActionResult> UpdateSection([FromRoute] int id, [FromRoute] int categoryId,
        [FromBody] UpdateSectionRequest updateSectionRequest)
    {
        await _sectionService.UpdateSectionAsync(id: id, categoryId: categoryId, dto: updateSectionRequest);
        return NoContent();
    }
    
    [Authorize]
    [SwaggerResponse(200, "List of Sections", typeof(IEnumerable<SectionListResponse>))]
    [HttpGet]
    public async Task<IActionResult> GetSections()
    {
        var categories = await _sectionService.GetSectionListAsync();
        return Ok(categories);
    }

    [Authorize]
    [SwaggerResponse(204, "Section order updated successfully")]
    [HttpPatch("order")]
    public async Task<IActionResult> UpdateSectionOrder([FromBody] List<OrderDto> dto)
    {
        await _sectionService.UpdateSectionOrderAsync(dto);
        return NoContent();
    }
}