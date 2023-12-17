using Dapper;
using Domain.DTOs.ManagerDto;
using Infrastructure.Context;
using Infrastructure.Interfaces;

namespace Infrastructure.Services;

public class ManagerService
{
    private readonly DapperContext _context;
    private readonly IFileService _fileService;

    public ManagerService(DapperContext context, IFileService fileService)
    {
        _context = context;
        _fileService = fileService;
    }

    //Get all Managers
    public async Task<IEnumerable<GManagerDto>> GetAllManagers()
    {
        using var conn = _context.CreateConnection();

        var command = " select m.managerid, concat(e.firstname, ' ', e.lastname) as managerfullname, m.departmentid, d.name as departmentname, m.fromdate, m.todate " +
                      " from managers m " +
                      " join employees e " +
                      " on m.managerid = e.id " +
                      " join departments d " +
                      " on m.departmentid = d.id;";

        var result = await conn.QueryAsync<GManagerDto>(command);

        return result;
    }

    //Get Manager by Id
    public async Task<GManagerDto> GetManagerById(int managerId, int departmentId)
    {
        using var conn = _context.CreateConnection();

        var command = " select m.managerid, concat(e.firstname, ' ', e.lastname) as managerfullname, m.departmentid, d.name as departmentname, m.fromdate, m.todate " +
                      " from managers m " +
                      " join employees e " +
                      " on m.managerid = e.id " +
                      " join departments d " +
                      " on m.departmentid = d.id " +
                      " where m.managerid = @ManagerId and m.departmentid = @DepartmentId;";

        var result = await conn.QuerySingleOrDefaultAsync<GManagerDto>(command, new
        {
            ManagerId = managerId,
            DepartmentId = departmentId
        });

        return result;
    }

    //Add Manager
    public async Task<GManagerDto> AddManager(AManagerDto manager) 
    {
        using var conn = _context.CreateConnection();

        string command = null;

        if (manager.ToDate != null)
        {
            //Add Manager within todate
            command = " insert into managers (managerid, departmentid, fromdate, todate) " +
                      " values (@ManagerId, @DepartmentId, @FromDate, @ToDate);";
        }    
        else
        {
            //Add Manager without todate
            command = " insert into managers (managerid, departmentid, fromdate) " +
                      " values (@ManagerId, @DepartmentId, @FromDate);";
        }

        await conn.ExecuteAsync(command, new
        {
            ManagerId = manager.ManagerId,
            DepartmentId = manager.DepartmentId,
            FromDate = manager.FromDate,
            ToDate = manager.ToDate,
        });

        return await GetManagerById(manager.ManagerId, manager.DepartmentId);
        /*return new GManagerDto
        {
            ManagerId = manager.ManagerId,
            DepartmentId = manager.DepartmentId,
            FromDate = manager.FromDate,
            ToDate = manager.ToDate,
        };*/
    }

    //Update Manager
    public async Task<GManagerDto> UpdateManager(UManagerDto manager)
    {
        using var conn = _context.CreateConnection();

        string command = null;

        if(manager.ToDate != null) 
        {
            command = " update managers " +
                      " set managerid = @NewManagerId, departmentid = @NewDepartmentId, fromdate = @FromDate, todate = @ToDate " +
                      " where managerid = @ManagerId and departmentid = @DepartmentId;";
        }
        else
        {
            command = " update managers " +
                      " set managerid = @NewManagerId, departmentid = @NewDepartmentId, fromdate = @FromDate " +
                      " where managerid = @ManagerId and departmentid = @DepartmentId;";
        }

        await conn.ExecuteAsync(command, new
        {
            NewManagerId = manager.NewManagerId,
            NewDepartmentId = manager.NewDepartmentId,
            ManagerId = manager.ManagerId,
            DepartmentId = manager.DepartmentId,
            FromDate = manager.FromDate,
            ToDate = manager.ToDate,
        });

        manager.ManagerId = manager.NewManagerId;
        manager.DepartmentId = manager.NewDepartmentId;

        return await GetManagerById(manager.ManagerId, manager.DepartmentId);
    }

    //Delete Manager
    public async Task<GManagerDto> DeleteManager(int managerId, int departmentId)
    {
        using var conn = _context.CreateConnection();

        var command = " delete from managers " +
                      " where managerid = @ManagerId and departmentid = @DepartmentId;";

        var deleted = await GetManagerById(managerId, departmentId);

        await conn.ExecuteAsync(command, new
        {
            ManagerId = managerId,
            DepartmentId = departmentId
        });

        return deleted;
    }
}
