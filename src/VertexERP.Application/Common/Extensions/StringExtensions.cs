namespace VertexERP.Application.Common.Extensions;

public static class StringExtensions
{
    public static string ToCleanString(this string name)
        => name.Trim().ToLowerInvariant();
}