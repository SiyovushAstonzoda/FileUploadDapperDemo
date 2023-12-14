using Microsoft.AspNetCore.Http;

namespace Domain.DTOs.EmployeeDto;

public class GEmployeeDto : EmployeeDto
{
    public string FileName { get; set; }
}