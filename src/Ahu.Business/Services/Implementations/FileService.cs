using Ahu.Business.Services.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Ahu.Business.Services.Implementations;

public class FileService : IFileService
{
    public async Task<string> CreateFileAsync(string path, IFormFile file)
    {
        string fileName = $"{Guid.NewGuid()} - {file.FileName}";

        string resultPath = Path.Combine(path, fileName);

        using (FileStream fileStream = new FileStream(resultPath, FileMode.Create))
        {
            await file.CopyToAsync(fileStream);
        }

        return fileName;
    }
}