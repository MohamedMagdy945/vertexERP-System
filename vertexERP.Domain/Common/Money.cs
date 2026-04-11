namespace vertexERP.Domain.Common
{
    public class Money
    {
        public decimal Amount { get; }
        public string Currency { get; }

        public Money(decimal amount, string currency)
        {
            if (amount < 0)
                throw new Exception("Invalid amount");

            Amount = amount;
            Currency = currency;
        }
    }
}
