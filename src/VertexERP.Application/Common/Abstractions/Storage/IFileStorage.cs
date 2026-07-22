using Microsoft.AspNetCore.Http;

namespace VertexERP.Application.Common.Abstractions.Storage;

public interface IFileStorage
{
    Task<string> UploadAsync(IFormFile file, string folder, CancellationToken cancellationToken);
    Task DeleteAsync(string filePath, CancellationToken cancellationToken);
}