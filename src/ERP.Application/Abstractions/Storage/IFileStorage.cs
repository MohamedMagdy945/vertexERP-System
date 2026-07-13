using Microsoft.AspNetCore.Http;

namespace VertexERP.Application.Abstractions.Storage;

public interface IFileStorage
{

    Task<string> UploadAsync(IFormFile file, string directory,
        CancellationToken cancellationToken = default);

    Task DeleteAsync(string filePath,
        CancellationToken cancellationToken = default);

}
