using Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;


[ApiController]
[Route("api/[controller]")]
public class ManagementController : ControllerBase
{
    private readonly IManagementService _managementService;

    public ManagementController(IManagementService managementService)
    {
        _managementService = managementService;
    }
}