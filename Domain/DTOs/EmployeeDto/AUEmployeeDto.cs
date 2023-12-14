using Microsoft.AspNetCore.Http;

namespace Domain.DTOs.EmployeeDto;

public class AUEmployeeDto : EmployeeDto
{
    public IFormFile? File { get; set; }
}
