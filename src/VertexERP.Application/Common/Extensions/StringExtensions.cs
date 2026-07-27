namespace VertexERP.Application.Common.Extensions;

public static class StringExtensions
{
    public static string FormatName(string name)
        => name.Trim().ToLowerInvariant();
}