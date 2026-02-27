using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using UseCases.FileStorageInterfaces;

namespace Plugins.FileStorage;

public class LocalFileStorage: IFileStorage
{
    private readonly IWebHostEnvironment _env;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public LocalFileStorage(IWebHostEnvironment env, IHttpContextAccessor httpContextAccessor)
    {
        _env = env;
        _httpContextAccessor = httpContextAccessor;
    }
    
    public async Task<string> Store(string container, IFormFile file)
    {
        var extension = Path.GetExtension(file.FileName);
        var fileName = $"{Guid.NewGuid()}{extension}";

        var folderPath = Path.Combine(_env.WebRootPath, container);

        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        var filePath = Path.Combine(folderPath, fileName);

        using (var ms = new MemoryStream())
        {
            await file.CopyToAsync(ms);
            var content = ms.ToArray();
            await File.WriteAllBytesAsync(filePath, content);
        }

        var request = _httpContextAccessor.HttpContext?.Request ?? throw new InvalidOperationException("HttpContext is null");
        var url = $"{request.Scheme}://{request.Host}";
        var fileUrl = Path.Combine(url, container, fileName).Replace("\\", "/");

        return fileUrl;
    }

    public Task Delete(string? route, string container)
    {
        if (string.IsNullOrEmpty(route))
        {
            return Task.CompletedTask;
        }

        var fileName = Path.GetFileName(route);
        var filePath = Path.Combine(_env.WebRootPath, container, fileName);

        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }

        return Task.CompletedTask;
    }
}