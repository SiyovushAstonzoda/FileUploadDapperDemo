using Microsoft.AspNetCore.Http;

namespace Domain.DTOs.SalaryDto;

public class AUSalaryDto : SalaryDto
{
    public IFormFile? File { get; set; }
}
