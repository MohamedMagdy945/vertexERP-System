using Mediator;
using VertexERP.Shared.Results;

namespace VertexERP.Application.Modules.Identity.Users.Commands.Create;


public sealed class Handler
    : IRequestHandler<Command, Result<Guid>>
{
    public async ValueTask<Result<Guid>> Handle(
        Command request, CancellationToken cancellationToken)
    {
        return Result<Guid>.Success(Guid.NewGuid());
    }
}