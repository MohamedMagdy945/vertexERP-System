namespace VertexERP.Shared.Constant;

public static class Permissions
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


    public static IReadOnlyList<string> GetAll()
        => [ Products.Read,Products.Create,Products.Update,Products.Delete
            , Categories.Read, Categories.Create, Categories.Update,Categories.Delete];
}