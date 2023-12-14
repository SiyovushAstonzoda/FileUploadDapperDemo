using Domain.DTOs.DepartmentDto;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace FUApi.Controllers;

[ApiController]
[Route("[controller]")]
public class DepartmentController
{
    private readonly DepartmentService _departmentService;

    public DepartmentController(DepartmentService departmentService)
    {
        _departmentService = departmentService;
    }

    [HttpPost("Add Department")]
    public GDepartmentDto AddDepartment([FromForm] AUDepartmentDto department)
    {
        return _departmentService.AddDepartment(department);
    }

    [HttpPut("Update Department")]
    public GDepartmentDto UpdateDepartment([FromForm] AUDepartmentDto department)
    {
        return _departmentService.UpdateDepartment(department);
    }

    [HttpDelete("Delete Department")]
    public GDepartmentDto DeleteDepartment(int id)
    {
        return _departmentService.DeleteDepartment(id);
    }

    [HttpGet("Get all Departments")]
    public IEnumerable<GDepartmentDto> GetAllDepartments()
    {
        return _departmentService.GetAllDepartments();
    }

    [HttpGet("Get Department by Id")]
    public GDepartmentDto GetDepartmentById(int id)
    {
        return _departmentService.GetDepartmentById(id);
    }
}
