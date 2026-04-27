using System.Text.RegularExpressions;

namespace VertexERP.Domain.Modules.Inventory.ValueObjects
{
    public class SKU
    {
        public string Value { get; }

        public SKU(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new Exception("SKU required");

            if (!Regex.IsMatch(value, "^[A-Z0-9-]+$"))
                throw new Exception("Invalid SKU format");

            Value = value;
        }
    }
}
