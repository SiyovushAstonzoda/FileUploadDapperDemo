using Domain.Enums;

namespace Domain.DTOs.EmployeeDto;

public class EmployeeDto
{
    public int Id { get; set; }
    public DateTime BirthDate { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public Gender Gender { get; set; } = Gender.Male;
    public DateTime HireDate { get; set; }
}
