    using Domain.DTOs.DepartmentManagerDto;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace FUApi.Controllers;

[ApiController]
[Route("[controller]")]
public class DepartmentManagerController : ControllerBase
{
    private readonly DepartmentManagerService _departmentManager;

    public DepartmentManagerController(DepartmentManagerService departmentManager)
    {
        _departmentManager = departmentManager;
    }

    [HttpPost("Add Department Manager")]
    public GDepartmentManagerDto AddDepartmentManager([FromForm] AUDepartmentManagerDto departmentManager)
    {
        return _departmentManager.AddDepartmentManager(departmentManager);
    }

    [HttpPut("Update Department Manager")]
    public GDepartmentManagerDto UpdateDepartmentManager([FromForm] AUDepartmentManagerDto departmentManager) 
    { 
        return _departmentManager.UpdateDepartmentManager(departmentManager);
    }

    [HttpDelete("Delete Department Manager")]
    public GDepartmentManagerDto DeleteDepartmentManager(int employeeId, int departmentId)
    {
        return _departmentManager.DeleteDepartmentManager(employeeId, departmentId);
    }

    [HttpGet("Get all Department Managers")]
    public IEnumerable<GDepartmentManagerDto> GetAllDepartmentManagers()
    {
        return _departmentManager.GetAllDepartmentManagers();
    }

    [HttpGet("Get Department Manager by Id")]
    public GDepartmentManagerDto GetDepartmentManagerById(int employeeId, int departmentId)
    {
        return _departmentManager.GetDepartmentManagerById(employeeId, departmentId);
    }
}
