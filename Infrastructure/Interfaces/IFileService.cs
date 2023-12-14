using Microsoft.AspNetCore.Http;

namespace Infrastructure.Interfaces;

public interface IFileService
{
    string CreateFile(string folderName, IFormFile file);
    bool DeleteFile(string folderName, string fileName); 
}
