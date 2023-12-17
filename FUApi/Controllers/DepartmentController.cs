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
    public async Task<GDepartmentDto> AddDepartment([FromForm] AUDepartmentDto department)
    {
        return await _departmentService.AddDepartment(department);
    }

    [HttpPut("Update Department")]
    public async Task<GDepartmentDto> UpdateDepartment([FromForm] AUDepartmentDto department)
    {
        return await _departmentService.UpdateDepartment(department);
    }

    [HttpDelete("Delete Department")]
    public async Task<GDepartmentDto> DeleteDepartment(int id)
    {
        return await _departmentService.DeleteDepartment(id);
    }

    [HttpGet("Get all Departments")]
    public async Task<IEnumerable<GDepartmentDto>> GetAllDepartments()
    {
        return await _departmentService.GetAllDepartments();
    }

    [HttpGet("Get Department by Id")]
    public async Task<GDepartmentDto> GetDepartmentById(int id)
    {
        return await _departmentService.GetDepartmentById(id);
    }
}
