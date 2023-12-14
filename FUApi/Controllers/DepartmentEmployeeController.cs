using Domain.DTOs.DepartmentEmployeeDto;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace FUApi.Controllers;

[ApiController]
[Route("[controller]")]
public class DepartmentEmployeeController : ControllerBase
{
    private readonly DepartmentEmployeeService _departmentEmployeeService;

    public DepartmentEmployeeController(DepartmentEmployeeService departmentEmployeeService)
    {
        _departmentEmployeeService = departmentEmployeeService;
    }

    [HttpPost("Add Department Employee")]
    public GDepartmentEmployeeDto AddDepartmentEmployee([FromForm] AUDepartmentEmployeeDto departmentEmployee)
    {
        return _departmentEmployeeService.AddDepartmentEmployee(departmentEmployee);
    }

    [HttpPut("Update Department Employee")]
    public GDepartmentEmployeeDto UpdateDepartmentEmployee([FromForm] AUDepartmentEmployeeDto departmentEmployee) 
    {
        return _departmentEmployeeService.UpdateDepartmentEmployee(departmentEmployee);
    }

    [HttpDelete("Delete Department Employee")]
    public GDepartmentEmployeeDto DeleteDepartmentEmployee(int employeeId, int departmentId)
    {
        return _departmentEmployeeService.DeleteDepartmentEmployee(employeeId, departmentId);
    }

    [HttpGet("Get all Department Employees")]
    public IEnumerable<GDepartmentEmployeeDto> GetAllDepartmentEmployees()
    {
        return _departmentEmployeeService.GetAllDepartmentEmployees();
    }

    [HttpGet("Get Department Employee by Id")]
    public GDepartmentEmployeeDto GetDepartmentEmployeeById(int employeeId, int departmentId) 
    {
        return _departmentEmployeeService.GetGDepartmentEmployeeById(employeeId, departmentId); 
    }
}
