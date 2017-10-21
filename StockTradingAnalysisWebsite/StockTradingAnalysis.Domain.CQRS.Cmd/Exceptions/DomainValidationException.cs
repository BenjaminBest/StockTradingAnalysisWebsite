namespace StockTradingAnalysis.Domain.CQRS.Cmd.Exceptions
{
    public class DomainValidationException : System.Exception
    {
        public string Property { get; private set; }

        public DomainValidationException(string property, string message)
            : base(message)
        {
            Property = property;
        }
    }
}