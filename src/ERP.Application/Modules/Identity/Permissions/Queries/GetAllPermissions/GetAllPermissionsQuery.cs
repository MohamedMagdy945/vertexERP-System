using MediatR;
using VertexERP.Shared.Results;

namespace VertexERP.Application.Modules.Identity.Permissions.Queries.GetAllPermissions;

public sealed record GetAllPermissionsQuery()
    : IRequest<Result<List<GetAllPermissionsQueryResponse>>>;

