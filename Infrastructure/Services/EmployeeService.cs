using Dapper;
using Domain.DTOs.EmployeeDto;
using Domain.Enums;
using Infrastructure.Context;
using Infrastructure.Interfaces;

namespace Infrastructure.Services;

public class EmployeeService
{
    private readonly DapperContext _context;
    private readonly IFileService _fileService;

    public EmployeeService(DapperContext context, IFileService fileService)
    {
        _context = context;
        _fileService = fileService;
    }

    //Get all Employees
    public IEnumerable<GEmployeeDto> GetAllEmployees()
    {
        using var conn = _context.CreateConnection();
        
        var command = " select * " +
                      " from employees;";

        var result = conn.Query<GEmployeeDto>(command);

        return result;
    }

    //Get Employee by Id
    public GEmployeeDto GetEmployeeById(int id) 
    {
        using var conn = _context.CreateConnection();

        var command = " select * " +
                      " from employees " +
                      " where id = @Id;";

        var result = conn.QuerySingleOrDefault<GEmployeeDto>(command, new
        {
            Id = id
        });

        return result;
    }

    //Get Employee with Department
    public IEnumerable<GEmployeeWDepartment> GetEmployeesWithDepartments()
    {
        using var conn = _context.CreateConnection();

        var command = " select e.id, concat(e.firstname, ' ', e.lastname) as fullname, e.filename, d.id as departmentid, d.name as departmentname " +
                      " from employees e " +
                      " join departmentemployees de " +
                      " on e.id = de.employeeid " +
                      " join departments d " +
                      " on de.departmentid = d.id;";

        var result = conn.Query<GEmployeeWDepartment>(command);

        return result;
    }

    //Get Employee with Department by Id
    public GEmployeeWDepartment GetEmployeeWithDepartmentById(int id)
    {
        using var conn = _context.CreateConnection();

        var command = " select e.id, concat(e.firstname, ' ', e.lastname) as fullname, e.filename, d.id as departmentid, d.name as departmentname " +
                      " from employees e " +
                      " join departmentemployees de " +
                      " on e.id = de.employeeid " +
                      " join departments d " +
                      " on de.departmentid = d.id " +
                      " where e.id = @Id;";

        var result = conn.QuerySingleOrDefault<GEmployeeWDepartment>(command, new
        {
            Id = id
        });

        return result;
    }

    //Add Employee
    public GEmployeeDto AddEmployee(AUEmployeeDto employee)
    {
        using var conn = _context.CreateConnection();

        string fileName = null;
        string command = null;

        if (employee.File != null)
        {
            command = " insert into employees (birthdate, firstname, lastname, gender, hiredate, filename) " +
                      " values (@BirthDate, @FirstName, @LastName, @Gender, @HireDate, @FileName) " +
                      " returning id;";

            fileName = _fileService.CreateFile(FolderType.Images, employee.File);
        }
        else
        {
            command = " insert into employees (birthdate, firstname, lastname, gender, hiredate) " +
                      " values (@BirthDate, @FirstName, @LastName, @Gender, @HireDate) " +
                      " returning id;";
        }

        var result = conn.ExecuteScalar<int>(command, new
        {
            BirthDate = employee.BirthDate,
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            Gender = employee.Gender,
            HireDate = employee.HireDate,
            FileName = fileName
        });

        return new GEmployeeDto()
        {
            BirthDate = employee.BirthDate,
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            Gender = employee.Gender,
            HireDate = employee.HireDate,
            FileName = fileName,
            Id = result
        };
    }

    //Update Employee
    public GEmployeeDto UpdateEmployee(AUEmployeeDto employee)
    {
        using var conn = _context.CreateConnection();

        //Existing in DB using GetEmployeeById
        var existing = GetEmployeeById(employee.Id);
        if (existing == null)
        {
            return new GEmployeeDto();
        }

        string fileName = existing.FileName;
        string sql = null;
        if (employee.File != null)
        {
            sql = " update employees " +
                  " set birthdate = @BirthDate, firstname = @FirstName, lastname = @LastName, gender = @Gender, hiredate = @HireDate, filename = @FileName " +
                  " where id = @Id" +
                  " returning id;";

            if (fileName != null)
            {
                _fileService.DeleteFile(FolderType.Images, fileName);
            }
                fileName = _fileService.CreateFile(FolderType.Images, employee.File);
        }
        else
        {
            sql = " update employees " +
                  " set birthdate = @BirthDate, firstname = @FirstName, lastname = @LastName, gender = @Gender, hiredate = @HireDate" +
                  " where id = @Id " +
                  " returning id;";
        }

        var result = conn.ExecuteScalar<int>(sql, new
        {
            BirthDate = employee.BirthDate,
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            Gender = employee.Gender,
            HireDate = employee.HireDate,
            FileName = fileName,
            Id = employee.Id
        });

        return new GEmployeeDto()
        {
            BirthDate = employee.BirthDate,
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            Gender = employee.Gender,
            HireDate = employee.HireDate,
            FileName = fileName,
            Id = result
        };
    }
    
    //Delete Employee
    public string DeleteEmployee(int id)
    {
        using var conn = _context.CreateConnection();

        var command = " delete from employees " +
                      " where id = @Id" +
                      " returning id;";

        var result = conn.ExecuteScalar<int>(command, new
        {
            Id = id
        });

        return $"Successfully deleted employee, with Id: {result}";
    }
}
