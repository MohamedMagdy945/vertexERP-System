namespace VertexERP.Application.Common.Abstractions.Persistence;

public interface IUserPermissionProvider
{
    IQueryable<string> Get(Guid userId);
}