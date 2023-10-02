using Microsoft.AspNetCore.Mvc;

namespace FUApi.Controllers;

[ApiController]
[Route("[controller]")]
public class FileUploadController : ControllerBase 
{
    private readonly IWebHostEnvironment _webHostEnvironment;

    public FileUploadController(IWebHostEnvironment webHostEnvironment)
    {
        _webHostEnvironment = webHostEnvironment;
    }

    [HttpPost("Upload File")]
    public string UploadFile(IFormFile file)
    {
        var currentFolder = _webHostEnvironment.ContentRootPath;

        return currentFolder;
    }
}
