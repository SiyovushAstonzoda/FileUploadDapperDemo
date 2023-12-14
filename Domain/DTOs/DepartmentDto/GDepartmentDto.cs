namespace Domain.DTOs.DepartmentDto;

public class GDepartmentDto : DepartmentDto
{
    public int ManagerId { get; set; }
    public string ManagerFullname { get; set; }
    public string FileName { get; set; }
}
