using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Services;

public class FileService : IFileService
{
    private readonly IWebHostEnvironment _webHostEnvironment;

    public FileService(IWebHostEnvironment webHostEnvironment)
    {
        _webHostEnvironment = webHostEnvironment;
    }

    public async Task<string> CreateFileAsync(string folderName, IFormFile file)
    {
        var fileName = Guid.NewGuid().ToString()+Path.GetExtension(file.FileName);
        var fullPath = Path.Combine(_webHostEnvironment.WebRootPath, folderName, fileName);
        using var stream = new FileStream(fullPath, FileMode.Create);
        await file.CopyToAsync(stream);
        return fileName;
    }

    public bool DeleteFile(string folderName, string fileName)
    {
        var fullPath = Path.Combine(_webHostEnvironment.WebRootPath, folderName, fileName);
        if(File.Exists(fullPath))
        {
            File.Delete(fullPath);
            return true;
        }
        else
        {
            return false;
        }
    }
}
