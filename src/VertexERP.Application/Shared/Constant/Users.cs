namespace VertexERP.Application.Shared.Constant;

public static class SystemUsers
{
    public const string User = "user";
    public const string Admin = "admin";
    public const string System = "system";
    public const string Security = "security";
    public static IReadOnlyList<string> All() => [System, Security, Admin, User];
}