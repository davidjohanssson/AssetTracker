namespace Server
{
    public class CreateCurrencyDto
    {
        public string Name { get; set; }
        public double ExchangeRateRelativeToDollar { get; set; }
    }
}