using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace VertexERP.Application.Common.Extensions;

public static class DbUpdateExceptionExtensions
{
    public static bool IsUniqueConstraintViolation(this DbUpdateException ex) =>
        ex.InnerException is SqlException { Number: 2601 or 2627 };

    public static bool IsForeignKeyConstraintViolation(this DbUpdateException ex) =>
        ex.InnerException is SqlException { Number: 547 };
}
