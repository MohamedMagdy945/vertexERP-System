using Microsoft.AspNetCore.Http;

namespace VertexERP.Application.Common.Abstractions.Storage;

public interface IFileStorage
{
    Task<string> UploadAsync(IFormFile file, string directory, CancellationToken cancellationToken);
    Task DeleteAsync(string filePath, CancellationToken cancellationToken);
    Task<List<string>> UploadManyAsync(IEnumerable<IFormFile> files, string directory, CancellationToken cancellationToken = default);
    Task DeleteManyAsync(IEnumerable<string> filePaths, CancellationToken cancellationToken = default);
}