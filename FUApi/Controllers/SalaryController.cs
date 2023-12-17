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
    public async Task<GSalaryDto> AddSalary([FromForm] AUSalaryDto salary)
    {
        return await _salaryService.AddSalary(salary);
    }

    [HttpPut("Update Salary")]
    public async Task<GSalaryDto> UpdateSalary([FromForm] AUSalaryDto salary)
    {
        return await _salaryService.UpdateSalary(salary);
    }

    [HttpDelete("Delete Salary")]
    public async Task<string> DeleteSalary(int id)
    {
        return await _salaryService.DeleteSalary(id);
    }

    [HttpGet("Get all Salaries")]
    public async Task<IEnumerable<GSalaryDto>> GetAllSalaries()
    {
        return await _salaryService.GetAllSalaries();
    }

    [HttpGet("Get Salary by Id")]
    public async Task<GSalaryDto> GetSalaryById(int id)
    {
        return await _salaryService.GetSalaryById(id);
    }
}
