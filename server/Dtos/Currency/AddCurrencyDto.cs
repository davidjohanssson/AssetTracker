namespace server
{
    public class AddCurrencyDto
    {
        public string Name { get; set; }
        public double ExchangeRateRelativeToDollar { get; set; }
    }
}