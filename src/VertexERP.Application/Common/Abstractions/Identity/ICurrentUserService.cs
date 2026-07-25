namespace VertexERP.Application.Common.Abstractions.Identity;

public interface ICurrentUserService
{
    Guid UserId { get; }

    string? Email { get; }

    string? Name { get; }

    bool IsAuthenticated { get; }

    bool IsInRole(string role);
}