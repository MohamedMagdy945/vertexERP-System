namespace VertexERP.Application.Abstractions.Storage;

public interface IFileStorage
{

    Task<string> UploadAsync(Stream stream, string fileName, string contentType, string directory,
        CancellationToken cancellationToken = default);

    Task DeleteAsync(string filePath,
        CancellationToken cancellationToken = default);

}
