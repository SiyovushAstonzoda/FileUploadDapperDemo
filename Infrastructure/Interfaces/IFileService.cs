using Microsoft.AspNetCore.Http;

namespace Infrastructure.Interfaces;

public interface IFileService
{
    Task<string> CreateFileAsync(string folderName, IFormFile file);
    bool DeleteFile(string folderName, string fileName); 
}
