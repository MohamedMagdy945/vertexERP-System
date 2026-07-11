using Microsoft.AspNetCore.Hosting;
using VertexERP.Application.Abstractions.Storage;

namespace VertexERP.Infrastructure.Storage;

public class LocalFileStorage : IFileStorage
{
    private readonly IWebHostEnvironment _environment;

    public LocalFileStorage(IWebHostEnvironment environment)
    {
        _environment = environment;
    }

    public async Task<string> UploadAsync(Stream stream, string fileName, string contentType,
        string directory,
        CancellationToken cancellationToken = default)
    {
        var uploadPath = Path.Combine(
            _environment.WebRootPath,
            directory);

        Directory.CreateDirectory(uploadPath);

        var extension = Path.GetExtension(fileName);
        var uniqueName = $"{Guid.NewGuid()}{extension}";

        var fullPath = Path.Combine(uploadPath, uniqueName);

        await using var fileStream = new FileStream(fullPath, FileMode.Create);

        await stream.CopyToAsync(fileStream, cancellationToken);

        return $"/{directory.Replace("\\", "/")}/{uniqueName}";
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