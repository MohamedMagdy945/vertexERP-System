namespace VertexERP.Domain.Common.ValueObjects
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
        public Quantity Reduce(int amount)
        {
            if (amount <= 0)
                throw new Exception("Amount must be greater than zero");
            if (Value < amount)
                throw new Exception("Insufficient quantity");
            return new Quantity(Value - amount);
        }
    }
}
