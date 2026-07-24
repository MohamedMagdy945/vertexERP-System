using VertexERP.Domain.Module.Identity.Entities;

namespace VertexERP.Application.Modules.Identity.Authentication.Refresh;

public sealed record Context(RefreshToken RefreshToken, Guid UserId
                , string UserEmail, IReadOnlyCollection<string> Roles
);
