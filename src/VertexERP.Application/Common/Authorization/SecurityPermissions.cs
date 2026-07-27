

using System.Reflection;
namespace VertexERP.Application.Common.Authorization;

public static class SecurityPermissions
{
    public static class Products
    {
        public const string View = "products.view";
        public const string Manage = "products.manage";
    }

    public static class Categories
    {
        public const string View = "categories.view";
        public const string Manage = "categories.manage";
    }

    public static class Units
    {
        public const string View = "units.view";
        public const string Manage = "units.manage";
    }

    public static IReadOnlySet<string> All { get; } =
        typeof(SecurityPermissions)
            .GetNestedTypes()
            .SelectMany(t => t.GetFields(
                BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy))
            .Where(f => f.IsLiteral && !f.IsInitOnly && f.FieldType == typeof(string))
            .Select(f => (string)f.GetRawConstantValue()!)
            .ToHashSet();
}