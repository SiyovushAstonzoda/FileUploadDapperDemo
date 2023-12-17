using Dapper;
using Domain.DTOs.SalaryDto;
using Domain.Enums;
using Infrastructure.Context;
using Infrastructure.Interfaces;

namespace Infrastructure.Services;

public class SalaryService
{
    private readonly DapperContext _context;
    private readonly IFileService _fileService;

    public SalaryService(DapperContext context, IFileService fileService)
    {
        _context = context;
        _fileService = fileService;
    }

    //Get all Salaries
    public async Task<IEnumerable<GSalaryDto>> GetAllSalaries()
    {
        using var conn = _context.CreateConnection();

        var command = " select * " +
                      " from salaries;";

        var result = await conn.QueryAsync<GSalaryDto>(command);

        return result;
    }

    //Get Salary by Id
    public async Task<GSalaryDto> GetSalaryById(int id) 
    {
        using var conn = _context.CreateConnection();

        var command = " select * " +
                      " from salaries " +
                      " where id = @Id;";

        var result = await conn.QuerySingleOrDefaultAsync<GSalaryDto>(command, new
        {
            Id = id
        });

        return result;
    }

    //Add Salary
    public async Task<GSalaryDto> AddSalary(AUSalaryDto salary)
    {
        using var conn = _context.CreateConnection();

        string fileName = null;
        string command = null;

        if (salary.File != null)
        {
            command = " insert into salaries (amount, fromdate, todate, employeeid, filename) " +
                      " values (@Amount, @FromDate, @ToDate, @EmployeeId, @FileName) " +
                      " returning id;";

            fileName = await _fileService.CreateFileAsync(FolderType.Images, salary.File);
        }
        else
        {
            command = " insert into salaries (amount, fromdate, todate, employeeid) " +
                      " values (@Amount, @FromDate, @ToDate, @EmployeeId) " +
                      " returning id;";
        }

        var result = await conn.ExecuteScalarAsync<int>(command, new
        {
            Amount = salary.Amount,
            FromDate = salary.FromDate,
            ToDate = salary.ToDate,
            EmployeeId = salary.EmployeeId,
            FileName = fileName
        });

        return new GSalaryDto()
        {
            Amount = salary.Amount,
            FromDate = salary.FromDate,
            ToDate = salary.ToDate,
            EmployeeId = salary.EmployeeId,
            FileName = fileName,
            Id = result
        };
    }

    //Update Salary
    public async Task<GSalaryDto> UpdateSalary(AUSalaryDto salary) 
    {
        using var conn = _context.CreateConnection();

        //Existing in DB using GetSalaryById
        var existing = await GetSalaryById(salary.Id);
        if (existing == null)
        {
            return new GSalaryDto();
        }

        string fileName = null;
        string command = null;
        if (salary.File != null)
        {
            command = " update salaries " +
                      " set amount = @Amount, fromdate = @FromDate, todate = @ToDate, employeeid = @EmployeeId, filename = @FileName " +
                      " where id = @Id" +
                      " returning id;";

            if(fileName != null)
            {
                _fileService.DeleteFile(FolderType.Images, fileName);
            }

            fileName = await _fileService.CreateFileAsync(FolderType.Images, salary.File);
        }
        else
        {
            command = " update salaries " +
                      " set amount = @Amount, fromdate = @FromDate, todate = @ToDate, employeeid = @EmployeeId " +
                      " where id = @Id" +
                      " returning id;";
        }

        var result = await conn.ExecuteScalarAsync<int>(command, new
        {
            Amount = salary.Amount,
            FromDate = salary.FromDate,
            ToDate = salary.ToDate,
            EmployeeId = salary.EmployeeId,
            FileName = fileName,
            Id = salary.Id
        });

        return new GSalaryDto()
        {
            Amount = salary.Amount,
            FromDate = salary.FromDate,
            ToDate = salary.ToDate,
            EmployeeId = salary.EmployeeId,
            FileName = fileName,
            Id = result
        };
    }

    //Delete Salary
    public async Task<string> DeleteSalary(int id)
    {
        using var conn = _context.CreateConnection();

        var command = " delete from salaries " +
                      " where id = @Id" +
                      " returning id;";

        var result = await conn.ExecuteScalarAsync<int>(command, new
        {
            Id = id
        });

        return $"Successfully deleted salary, with Id: {result}";
    }
}
