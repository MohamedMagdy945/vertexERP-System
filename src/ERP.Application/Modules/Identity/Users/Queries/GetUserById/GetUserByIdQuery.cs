using MediatR;
using VertexERP.Shared.Results;

namespace VertexERP.Application.Modules.Identity.Users.Queries.GetUserById;

public record GetUserByIdQuery(int Id)
    : IRequest<Result<GetUserByIdQueryResponse>>;