using Microsoft.AspNetCore.Http;

namespace Domain.DTOs.DepartmentDto;

public class AUDepartmentDto : DepartmentDto
{
    public IFormFile? File { get; set; }
}
