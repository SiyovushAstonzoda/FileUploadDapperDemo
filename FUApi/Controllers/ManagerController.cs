using Domain.DTOs.ManagerDto;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace FUApi.Controllers;

[ApiController]
[Route("[controller]")]
public class ManagerController : ControllerBase
{
    private readonly ManagerService _managerService;

    public ManagerController(ManagerService managerService)
    {
        _managerService = managerService;
    }

    [HttpPost("Add Manager")]
    public GManagerDto AddManager([FromForm] AManagerDto manager)
    {
        return _managerService.AddManager(manager);
    }

    [HttpPut("Update Manager")]
    public GManagerDto UpdateManager([FromForm] UManagerDto manager) 
    {
        return _managerService.UpdateManager(manager);
    }

    [HttpDelete("Delete Manager")]
    public GManagerDto DeleteManager(int managerId, int departmentId) 
    {
        return _managerService.DeleteManager(managerId, departmentId);
    }

    [HttpGet("Get all Managers")]
    public IEnumerable<GManagerDto> GetAllManagers()
    {
        return _managerService.GetAllManagers();
    }

    [HttpGet("Get Manager by Id")]
    public GManagerDto GetManagerById(int managerId, int departmentId) 
    {
        return _managerService.GetManagerById(managerId, departmentId);
    }
}
