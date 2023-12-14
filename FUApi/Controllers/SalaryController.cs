using Domain.DTOs.SalaryDto;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace FUApi.Controllers;

[ApiController]
[Route("[controller]")]
public class SalaryController : ControllerBase
{
    private readonly SalaryService _salaryService;

    public SalaryController(SalaryService salaryService)
    {
        _salaryService = salaryService;
    }

    [HttpPost("Add Salary")]
    public GSalaryDto AddSalary([FromForm] AUSalaryDto salary)
    {
        return _salaryService.AddSalary(salary);
    }

    [HttpPut("Update Salary")]
    public GSalaryDto UpdateSalary([FromForm] AUSalaryDto salary)
    {
        return _salaryService.UpdateSalary(salary);
    }

    [HttpDelete("Delete Salary")]
    public string DeleteSalary(int id)
    {
        return _salaryService.DeleteSalary(id);
    }

    [HttpGet("Get all Salaries")]
    public IEnumerable<GSalaryDto> GetAllSalaries()
    {
        return _salaryService.GetAllSalaries();
    }

    [HttpGet("Get Salary by Id")]
    public GSalaryDto GetSalaryById(int id)
    {
        return _salaryService.GetSalaryById(id);
    }
}
