using Microsoft.AspNetCore.Http;

namespace Ahu.Business.Services.Interfaces;

public interface IFileService
{
    Task<string> CreateFileAsync(string path, IFormFile file);
}