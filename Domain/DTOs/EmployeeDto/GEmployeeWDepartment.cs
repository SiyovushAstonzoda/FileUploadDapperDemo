using Microsoft.AspNetCore.Http;

namespace Domain.DTOs.EmployeeDto;

public class GEmployeeWDepartment
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public string FileName { get; set; }
    public int DepartmentId { get; set; }
    public string DepartmentName { get; set; }
}
