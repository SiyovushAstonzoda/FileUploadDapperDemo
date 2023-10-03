﻿using Microsoft.AspNetCore.Mvc;

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
        var currentFolder = _webHostEnvironment.WebRootPath;
        var fullPath = Path.Combine(currentFolder, "images", file.FileName);
        //var exists = System.IO.File.Exists(fullPath); 
        using (var stream = new FileStream(fullPath, FileMode.Create))
        {
            file.CopyTo(stream);
        }
            return fullPath;
    }

    [HttpGet("Get List of Files")]
    public IEnumerable<string> GetListOfFiles()
    {
        var files = new List<string>();

        var fullPath = Path.Combine(_webHostEnvironment.WebRootPath, "images");

        var listOfDirectories = Directory.GetDirectories(fullPath);

        foreach(var directory in listOfDirectories) 
        {
            files.Add(directory);

            var directoryPath = Path.Combine(fullPath, directory);

            var listOfDirectoryFiles = Directory.GetFiles(directoryPath);

            files.AddRange(listOfDirectoryFiles);
        }

        var listOfFiles = Directory.GetFiles(fullPath);

        files.AddRange(listOfFiles);

        return files;
    }
}