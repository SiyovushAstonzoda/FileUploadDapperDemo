using Dapper;
using Domain.DTOs.DepartmentEmployeeDto;
using Infrastructure.Context;

namespace Infrastructure.Services;

public class DepartmentEmployeeService
{
    private readonly DapperContext _context;

    public DepartmentEmployeeService(DapperContext context)
    {
        _context = context;
    }

    //Get all Department Employees
    public IEnumerable<GDepartmentEmployeeDto> GetAllDepartmentEmployees()
    {
        using var conn = _context.CreateConnection();

        var command = " select * " +
                      " from departmentemployees;";

        var result = conn.Query<GDepartmentEmployeeDto>(command);

        return result;
    }

    //Get Department Employee by EmployeeId and DepartmentId
    public GDepartmentEmployeeDto GetGDepartmentEmployeeById(int employeeId, int departmentId)
    {
        using var conn = _context.CreateConnection();

        var command = " select * " +
                      " from departmentemployees " +
                      " where employeeid = @EmployeeId and departmentid = @DepartmentId;";

        var result = conn.QuerySingleOrDefault<GDepartmentEmployeeDto>(command, new
        {
            EmployeeId = employeeId,
            DepartmentId = departmentId
        });

        return result;
    }

    //Add Department Employee
    public GDepartmentEmployeeDto AddDepartmentEmployee(AUDepartmentEmployeeDto departmentEmployee)
    {
        using var conn = _context.CreateConnection();

        var existing = GetGDepartmentEmployeeById(departmentEmployee.EmployeeId, departmentEmployee.DepartmentId);
        if(existing != null)
        {
            return new GDepartmentEmployeeDto();
        }

        var command = " insert into departmentemployees (employeeid, departmentid, fromdate, todate) " +
                      " values (@EmployeeId, @DepartmentId, @FromDate, @ToDate);";

        conn.Execute(command, new
        {
            EmployeeId = departmentEmployee.EmployeeId,
            DepartmentId = departmentEmployee.DepartmentId,
            FromDate = departmentEmployee.FromDate,
            ToDate = departmentEmployee.ToDate
        });

        return new GDepartmentEmployeeDto()
        {
            EmployeeId = departmentEmployee.EmployeeId,
            DepartmentId = departmentEmployee.DepartmentId,
            FromDate = departmentEmployee.FromDate,
            ToDate = departmentEmployee.ToDate
        };
    }

    //Update Department Employee
    public GDepartmentEmployeeDto UpdateDepartmentEmployee(AUDepartmentEmployeeDto departmentEmployee)
    {
        using var conn = _context.CreateConnection();

        var existing = GetGDepartmentEmployeeById(departmentEmployee.EmployeeId, departmentEmployee.DepartmentId);

        if (existing == null)
        {
            return new GDepartmentEmployeeDto();
        }

        var command = " update departmentemployees " +
                      " set employeeid = @EmployeeId, departmentid = @DepartmentId, fromdate = @FromDate, todate = @ToDate " +
                      " where employeeid = @EmployeeId and departmentid = @DepartmentId;";

        conn.Execute(command, new
        {
            EmployeeId = departmentEmployee.EmployeeId,
            DepartmentId = departmentEmployee.DepartmentId,
            FromDate = departmentEmployee.FromDate,
            ToDate = departmentEmployee.ToDate,
        });

        return new GDepartmentEmployeeDto()
        {
            EmployeeId = departmentEmployee.EmployeeId,
            DepartmentId = departmentEmployee.DepartmentId,
            FromDate = departmentEmployee.FromDate,
            ToDate = departmentEmployee.ToDate,
        };
    }

    //Delete epartment Employee
    public GDepartmentEmployeeDto DeleteDepartmentEmployee(int employeeId, int departmentId)
    {
        using var conn = _context.CreateConnection();

        var deleted = GetGDepartmentEmployeeById(employeeId, departmentId);

        var command = " delete from departmentemployees " +
                      " where employeeid = @EmployeeId and departmentid = @DepartmentId;";

        conn.Execute(command, new
        {
            Employeeid = employeeId,
            DepartmentId = departmentId,
        });

        return deleted;
    }
}
