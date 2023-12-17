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
    public async Task<GManagerDto> AddManager([FromForm] AManagerDto manager)
    {
        return await _managerService.AddManager(manager);
    }

    [HttpPut("Update Manager")]
    public async Task<GManagerDto> UpdateManager([FromForm] UManagerDto manager) 
    {
        return await _managerService.UpdateManager(manager);
    }

    [HttpDelete("Delete Manager")]
    public async Task<GManagerDto> DeleteManager(int managerId, int departmentId) 
    {
        return await _managerService.DeleteManager(managerId, departmentId);
    }

    [HttpGet("Get all Managers")]
    public async Task<IEnumerable<GManagerDto>> GetAllManagers()
    {
        return await _managerService.GetAllManagers();
    }

    [HttpGet("Get Manager by Id")]
    public async Task<GManagerDto> GetManagerById(int managerId, int departmentId) 
    {
        return await _managerService.GetManagerById(managerId, departmentId);
    }
}
