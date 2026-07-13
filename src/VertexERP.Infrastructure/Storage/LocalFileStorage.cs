using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using VertexERP.Application.Abstractions.Storage;

namespace VertexERP.Infrastructure.Storage;

public class LocalFileStorage : IFileStorage
{
    private readonly IWebHostEnvironment _environment;

    public LocalFileStorage(IWebHostEnvironment environment)
    {
        _environment = environment;
    }

    public async Task<string> UploadAsync(IFormFile file, string directory, CancellationToken cancellationToken = default)
    {
        var uploadPath = Path.Combine(_environment.WebRootPath, directory);

        Directory.CreateDirectory(uploadPath);

        var extension = Path.GetExtension(file.FileName);
        var uniqueName = $"{Guid.NewGuid()}{extension}";

        var fullPath = Path.Combine(uploadPath, uniqueName);

        await using var fileStream = new FileStream(fullPath, FileMode.Create);
        await file.CopyToAsync(fileStream, cancellationToken);

        var webPath = directory.Replace("\\", "/").Trim('/');
        return $"/{webPath}/{uniqueName}";
    }

    public Task DeleteAsync(string filePath, CancellationToken cancellationToken = default)
    {
        var fullPath = Path.Combine(
            _environment.WebRootPath,
            filePath.TrimStart('/'));

        if (File.Exists(fullPath))
        {
            File.Delete(fullPath);
        }

        return Task.CompletedTask;
    }
}