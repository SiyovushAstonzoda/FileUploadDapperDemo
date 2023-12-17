using Dapper;
using Domain.DTOs.DepartmentDto;
using Domain.Enums;
using Infrastructure.Context;
using Infrastructure.Interfaces;

namespace Infrastructure.Services;

public class DepartmentService
{
    private readonly DapperContext _context;
    private readonly IFileService _fileService;

    public DepartmentService(DapperContext context, IFileService fileService)
    {
        _context = context;
        _fileService = fileService;
    }

    //Get all Departments
    public async Task<IEnumerable<GDepartmentDto>> GetAllDepartments()
    {
        using var conn = _context.CreateConnection();

        var command = " select d.id, d.name, m.managerid, concat(e.firstname, ' ', e.lastname) as managerfullname, d.filename " +
                      " from departments d " +
                      " left join managers m " +
                      " on d.id = m.departmentid " +
                      " left join employees e " +
                      " on m.managerid = e.id " +
                      " order by d.id;";

        var result = await conn.QueryAsync<GDepartmentDto>(command);

        return result;
    }

    //Get Department by Id
    public async Task<GDepartmentDto> GetDepartmentById(int id)
    {
        using var conn = _context.CreateConnection();

        var command = " select d.id, d.name, m.managerid, concat(e.firstname, ' ', e.lastname) as managerfullname, d.filename " +
                      " from departments d " +
                      " left join managers m " +
                      " on d.id = m.departmentid " +
                      " left join employees e " +
                      " on m.managerid = e.id " +
                      " where d.id = @Id";

        var result = await conn.QuerySingleOrDefaultAsync<GDepartmentDto>(command, new
        {
            Id = id
        });

        return result;
    }

    //Add Department
    public async Task<GDepartmentDto> AddDepartment(AUDepartmentDto department)
    {
        using var conn = _context.CreateConnection();

        string fileName = null;
        string command = null;

        if(department.File != null)
        {
            command = " insert into departments (name, filename) " +
                      " values (@Name, @FileName)" +
                      " returning id;";

            fileName = await _fileService.CreateFileAsync(FolderType.Images, department.File);
        }
        else
        {
            command = " insert into departments (name) " +
                      " values (@Name)" +
                      " returning id;";
        }

        var result = await conn.ExecuteScalarAsync<int>(command, new
        {
            Name = department.Name,
            FileName = fileName
        });

        return new GDepartmentDto()
        {
            Name = department.Name,
            FileName = fileName,
            Id = result,
            ManagerId = 0,
            ManagerFullname = null
        };
    }

    //Update Department
    public async Task<GDepartmentDto> UpdateDepartment(AUDepartmentDto department)
    {
        using var conn = _context.CreateConnection();

        //Existing in DB using GetDepartmentById
        var existing = GetDepartmentById(department.Id);
        if(existing == null) 
        {
            return new GDepartmentDto();
        }

        string fileName = null;
        string command = null;

        if(department.File != null)
        {
            command = " update departments " +
                      " set name = @Name, filename = @FileName " +
                      " where id = @Id " +
                      " returning id;";

            fileName = await _fileService.CreateFileAsync(FolderType.Images, department.File);
        }
        else
        {
            command = " update departments " +
                      " set name = @Name" +
                      " where id = @Id" +
                      " returning id;";
        }

        var result = await conn.ExecuteScalarAsync<int>(command, new
        {
            Name = department.Name,
            FileName = fileName,
            Id = department.Id
        });

        return new GDepartmentDto()
        {
            Name = department.Name,
            FileName = fileName,
            Id = result
        };
    }

    public async Task<GDepartmentDto> DeleteDepartment(int id)
    {
        using var conn = _context.CreateConnection();

        var command = " delete from departments " +
                      " where id = @Id " +
                      " returning id;";

        var deleted = await GetDepartmentById(id);

        var result = await conn.ExecuteScalarAsync<int>(command, new
        {
            Id = id
        });

        return deleted;
    }
}
