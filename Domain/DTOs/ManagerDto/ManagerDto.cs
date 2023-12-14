namespace Domain.DTOs.ManagerDto;

public abstract class ManagerDto
{
    public int ManagerId { get; set; }
    public int DepartmentId { get; set; }
    public DateTime FromDate { get; set; }
    public Nullable<DateTime> ToDate { get; set; } = null;
}
