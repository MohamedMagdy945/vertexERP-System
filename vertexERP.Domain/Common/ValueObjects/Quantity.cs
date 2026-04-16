namespace vertexERP.Domain.Common.ValueObjects
{
    public class Quantity
    {
        public int Value { get; }

        public Quantity(int value)
        {
            if (value < 0)
                throw new Exception("Quantity cannot be negative");

            Value = value;
        }
    }
}
