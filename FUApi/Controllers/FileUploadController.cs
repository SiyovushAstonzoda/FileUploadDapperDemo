using Microsoft.AspNetCore.Mvc;

namespace FUApi.Controllers;

[ApiController]
[Route("[controller]")]
public class FileUploadController : ControllerBase 
{
    public FileUploadController()
    {
        
    }

    [HttpPost("Upload File")]
    public string UploadFile(IFormFile file)
    {
        return "Ok";
    }
}
