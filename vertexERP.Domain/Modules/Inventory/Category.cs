using vertexERP.Domain.Common;

namespace vertexERP.Domain.Modules.Inventory
{
    public class Category : BaseEntity
    {
        public string Name { get; set; } = string.Empty;

        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
