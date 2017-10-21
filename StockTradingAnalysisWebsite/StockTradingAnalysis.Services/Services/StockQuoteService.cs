using StockTradingAnalysis.Interfaces.Services;

namespace StockTradingAnalysis.Services.Services
{
    public class StockQuoteService : IStockQuoteService
    {
        /// <summary>
        /// Returns the quote of the given stock by <paramref name="wkn"/>
        /// </summary>
        /// <param name="wkn">The wkn of the stock</param>
        public decimal GetQuote(string wkn)
        {
            //TODO: Implement StockQuotationService
            return 0;
        }

        /// <summary>
        /// Returns the average true range of the given stock by <paramref name="wkn"/>
        /// </summary>
        /// <param name="wkn">The average true range (atr) of the stock</param>
        public decimal GetAtr(string wkn)
        {
            //TODO: Implement StockQuotationService
            return 0;
        }
    }
}