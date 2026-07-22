using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using VertexERP.Application.Common.Abstractions.Storage;

namespace VertexERP.Infrastructure.Storage;

public sealed class LocalFileStorage(IWebHostEnvironment environment) : IFileStorage
{
    public async Task<string> UploadAsync(IFormFile file, string directory, CancellationToken cancellationToken = default)
    {
        var uploadDirectory = Path.Combine(environment.WebRootPath, directory);

        Directory.CreateDirectory(uploadDirectory);

        var extension = Path.GetExtension(file.FileName);

        var fileName = $"{Guid.NewGuid()}{extension}";

        var filePath = Path.Combine(uploadDirectory, fileName);

        await using var stream = new FileStream(filePath, FileMode.Create);

        await file.CopyToAsync(stream, cancellationToken);

        return Path.Combine(directory, fileName).Replace("\\", "/");
    }
    public Task DeleteAsync(string filePath, CancellationToken cancellationToken = default)
    {
        var fullPath = Path.Combine(environment.WebRootPath, filePath);

        if (File.Exists(fullPath))
            File.Delete(fullPath);

        return Task.CompletedTask;
    }
    public async Task<List<string>> UploadManyAsync(IEnumerable<IFormFile> files, string folderName, CancellationToken cancellationToken = default)
    {
        var uploadTasks = files.Select(file => UploadAsync(file, folderName, cancellationToken));
        var results = await Task.WhenAll(uploadTasks);
        return results.ToList();
    }
    public async Task DeleteManyAsync(IEnumerable<string> filePaths, CancellationToken cancellationToken = default)
    {
        if (filePaths is null || !filePaths.Any())
            return;

        var deleteTasks = filePaths.Where(path => !string.IsNullOrWhiteSpace(path))
            .Select(path => DeleteAsync(path));

        await Task.WhenAll(deleteTasks);
    }
}