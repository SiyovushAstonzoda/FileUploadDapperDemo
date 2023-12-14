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
    public IEnumerable<GSalaryDto> GetAllSalaries()
    {
        using var conn = _context.CreateConnection();

        var command = " select * " +
                      " from salaries;";

        var result = conn.Query<GSalaryDto>(command);

        return result;
    }

    //Get Salary by Id
    public GSalaryDto GetSalaryById(int id) 
    {
        using var conn = _context.CreateConnection();

        var command = " select * " +
                      " from salaries " +
                      " where id = @Id;";

        var result = conn.QuerySingleOrDefault<GSalaryDto>(command, new
        {
            Id = id
        });

        return result;
    }

    //Add Salary
    public GSalaryDto AddSalary(AUSalaryDto salary)
    {
        using var conn = _context.CreateConnection();

        string fileName = null;
        string command = null;

        if (salary.File != null)
        {
            command = " insert into salaries (amount, fromdate, todate, employeeid, filename) " +
                      " values (@Amount, @FromDate, @ToDate, @EmployeeId, @FileName) " +
                      " returning id;";

            fileName = _fileService.CreateFile(FolderType.Images, salary.File);
        }
        else
        {
            command = " insert into salaries (amount, fromdate, todate, employeeid) " +
                      " values (@Amount, @FromDate, @ToDate, @EmployeeId) " +
                      " returning id;";
        }

        var result = conn.ExecuteScalar<int>(command, new
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
    public GSalaryDto UpdateSalary(AUSalaryDto salary) 
    {
        using var conn = _context.CreateConnection();

        //Existing in DB using GetSalaryById
        var existing = GetSalaryById(salary.Id);
        if (existing == null)
        {
            return new GSalaryDto();
        }

        string fileName = existing.FileName;
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

            fileName = _fileService.CreateFile(FolderType.Images, salary.File);
        }
        else
        {
            command = " update salaries " +
                      " set amount = @Amount, fromdate = @FromDate, todate = @ToDate, employeeid = @EmployeeId " +
                      " where id = @Id" +
                      " returning id;";
        }

        var result = conn.ExecuteScalar<int>(command, new
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
    public string DeleteSalary(int id)
    {
        using var conn = _context.CreateConnection();

        var command = " delete from salaries " +
                      " where id = @Id" +
                      " returning id;";

        var result = conn.ExecuteScalar<int>(command, new
        {
            Id = id
        });

        return $"Successfully deleted salary, with Id: {result}";
    }
}
