namespace VertexERP.Shared.Constant;

public static class Users
{
    public const string Admin = "admin";
    public const string System = "system";
    public const string User = "user";
    public const string Security = "security";
    public static IReadOnlyList<string> GetAll() => [System, Security, Admin, User];
}