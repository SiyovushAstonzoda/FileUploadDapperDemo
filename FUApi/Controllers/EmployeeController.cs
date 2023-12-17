using Domain.DTOs.EmployeeDto;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace FUApi.Controllers;

[ApiController]
[Route("[controller]")]
public class EmployeeController : ControllerBase
{
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly EmployeeService _employeeService;

    public EmployeeController(IWebHostEnvironment webHostEnvironment, EmployeeService employeeService)
    {
        _webHostEnvironment = webHostEnvironment;
        _employeeService = employeeService;
    }

    [HttpPost("Add Employee")]
    public async Task<GEmployeeDto> AddEmployee([FromForm] AUEmployeeDto employee)
    {
        return await _employeeService.AddEmployee(employee);
    }

    [HttpPut("Update Employee")]
    public async Task<GEmployeeDto> UpdateEmployee([FromForm] AUEmployeeDto employee)
    {
        return await _employeeService.UpdateEmployee(employee);
    }

    [HttpDelete("Delete Employee")]
    public async Task<string> DeleteEmployee(int id)
    {
        return await _employeeService.DeleteEmployee(id); 
    }

    [HttpGet("Get all Employees")]
    public async Task<IEnumerable<GEmployeeDto>> GetListOfEmployees()
    {
        return await _employeeService.GetAllEmployees();
    }

    [HttpGet("Get Employee by Id")]
    public GEmployeeDto GetEmployeeById(int id) 
    {
        return _employeeService.GetEmployeeById(id);
    }

    [HttpGet("Get Employees with Departments")]
    public IEnumerable<GEmployeeWDepartment> GetEmployeesWithDeparyments()
    {
        return _employeeService.GetEmployeesWithDepartments();
    }

    [HttpGet("Get Employee with Department by Id")]
    public async Task<GEmployeeWDepartment> GetEmployeeWithDepartmentById(int id)
    {
        return await _employeeService.GetEmployeeWithDepartmentById(id);
    }
}
