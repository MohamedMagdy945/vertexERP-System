namespace VertexERP.Application.Shared.Authorization;

public static class Permission
{
    public static class Products
    {
        public const string Read = "products.read";
        public const string Create = "products.create";
        public const string Update = "products.update";
        public const string Delete = "products.delete";
    }

    public static class Categories
    {
        public const string Read = "categories.read";
        public const string Create = "categories.create";
        public const string Update = "categories.update";
        public const string Delete = "categories.delete";
    }

    public static IReadOnlySet<string> All { get; } =
        new HashSet<string>
        {
            Products.Read,
            Products.Create,
            Products.Update,
            Products.Delete,

            Categories.Read,
            Categories.Create,
            Categories.Update,
            Categories.Delete
        };
}