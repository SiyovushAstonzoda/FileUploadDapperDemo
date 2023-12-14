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
    public GEmployeeDto AddEmployee([FromForm] AUEmployeeDto employee)
    {
        return _employeeService.AddEmployee(employee);
    }

    [HttpPut("Update Employee")]
    public GEmployeeDto UpdateEmployee([FromForm] AUEmployeeDto employee)
    {
        return _employeeService.UpdateEmployee(employee);
    }

    [HttpDelete("Delete Employee")]
    public string DeleteEmployee(int id)
    {
        return _employeeService.DeleteEmployee(id); 
    }

    [HttpGet("Get all Employees")]
    public IEnumerable<GEmployeeDto> GetListOfEmployees()
    {
        return _employeeService.GetAllEmployees();
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
    public GEmployeeWDepartment GetEmployeeWithDepartmentById(int id)
    {
        return _employeeService.GetEmployeeWithDepartmentById(id);
    }
}
