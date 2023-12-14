using Dapper;
using Domain.DTOs.DepartmentManagerDto;
using Infrastructure.Context;

namespace Infrastructure.Services;

public class DepartmentManagerService
{
    private readonly DapperContext _context;

    public DepartmentManagerService(DapperContext context)
    {
        _context = context;
    }

    //Get all Department Manager
    public IEnumerable<GDepartmentManagerDto> GetAllDepartmentManagers()
    {
        using var conn = _context.CreateConnection();

        var command = " select * " +
                      " from departmentmanagers;";

        var result = conn.Query<GDepartmentManagerDto>(command);

        return result;
    }

    //Get Department Manager by Id
    public GDepartmentManagerDto GetDepartmentManagerById(int employeeId, int departmentId)
    {
        using var conn = _context.CreateConnection();

        var command = " select * from departmentmanagers " +
                      " where employeeid = @EmployeeId and departmentid = @DepartmentId;";

        var result = conn.QuerySingleOrDefault<GDepartmentManagerDto>(command, new
        {
            EmployeeId = employeeId,
            DepartmentId = departmentId
        });

        return result;
    }

    //Add Department Manager
    public GDepartmentManagerDto AddDepartmentManager(AUDepartmentManagerDto departmentManager)
    {
        using var conn = _context.CreateConnection();

        var existing = GetDepartmentManagerById(departmentManager.EmployeeId, departmentManager.DepartmentId);

        if (existing != null) 
        {
            return new GDepartmentManagerDto();
        }

        var command = " insert into departmentmanagers (employeeid, departmentid, fromdate, todate) " +
                      " values (@EmployeeId, @DepartmentId, @FromDate, @ToDate);";

        conn.Execute(command, new
        {
            EmployeeId = departmentManager.EmployeeId,
            DepartmentId = departmentManager.DepartmentId,
            FromDate = departmentManager.FromDate,
            ToDate = departmentManager.ToDate,
        });

        return new GDepartmentManagerDto()
        {
            EmployeeId = departmentManager.EmployeeId,
            DepartmentId = departmentManager.DepartmentId,
            FromDate = departmentManager.FromDate,
            ToDate = departmentManager.ToDate,
        };
    }

    //Update Department Manager
    public GDepartmentManagerDto UpdateDepartmentManager(AUDepartmentManagerDto departmentManager)
    {
        using var conn = _context.CreateConnection();

        var existing = GetDepartmentManagerById(departmentManager.EmployeeId, departmentManager.DepartmentId);

        if(existing == null)
        {
            return new GDepartmentManagerDto();
        }

        var command = " update departmentmanagers " +
                      " set employeeid = @EmployeeId, departmentid = @DepartmentId, fromdate = @FromDate, todate = @ToDate " +
                      " where employeeid = @EmployeeId and departmentid = @DepartmentId;";

        conn.Execute(command, new
        {
            EmployeeId = departmentManager.EmployeeId,
            DepartmentId = departmentManager.DepartmentId,
            FromDate = departmentManager.FromDate,
            ToDate = departmentManager.ToDate,
        });

        return new GDepartmentManagerDto()
        {
            EmployeeId = departmentManager.EmployeeId,
            DepartmentId = departmentManager.DepartmentId,
            FromDate = departmentManager.FromDate,
            ToDate = departmentManager.ToDate,
        };
    }

    //Delete Department Manager
    public GDepartmentManagerDto DeleteDepartmentManager(int employeeId, int departmentId)
    {
        using var conn = _context.CreateConnection();

        var deleted = GetDepartmentManagerById(employeeId, departmentId);

        var command = " delete from departmentmanagers " +
                      " where employeeid = @EmployeeId and departmentid = @DepartmentId;";

        conn.Execute(command, new
        {
            EmployeeId = employeeId,
            DepartmentId = departmentId,
        });

        return deleted;
    }
}
