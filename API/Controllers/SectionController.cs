using Application.Dto.Section;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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

    [HttpPost("/api/category/{categoryId}/[controller]")]
    public async Task<IActionResult> CreateSection([FromRoute] Guid categoryId,
        [FromBody] CreateSectionRequest createSectionRequest)
    {
        var section = await _sectionService.CreateSectionAsync(categoryId: categoryId,
            createSectionRequest: createSectionRequest);
        return Ok(section);
    }
    
    [Authorize]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetSectionById([FromRoute] Guid id)
    {
        var section = await _sectionService.GetSectionByIdAsync(sectionId: id);
        return Ok(section);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSectionById([FromRoute] Guid id)
    {
        await _sectionService.DeleteSectionAsync(id: id);
        return NoContent();
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateSection([FromRoute] Guid id,
        [FromBody] UpdateSectionRequest updateSectionRequest)
    {
        await _sectionService.UpdateSectionAsync(id: id, dto: updateSectionRequest);
        return NoContent();
    }
}