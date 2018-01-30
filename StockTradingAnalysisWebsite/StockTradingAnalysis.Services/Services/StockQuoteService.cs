using System.Linq;
using StockTradingAnalysis.Domain.CQRS.Query.Queries;
using StockTradingAnalysis.Interfaces.Queries;
using StockTradingAnalysis.Interfaces.Services.Domain;

namespace StockTradingAnalysis.Services.Services
{
    /// <summary>
    /// The StockQuoteService returns the current quotation for a given wkn
    /// </summary>
    /// <seealso cref="IStockQuoteService" />
    public class StockQuoteService : IStockQuoteService
    {
        /// <summary>
        /// The query dispatcher
        /// </summary>
        private readonly IQueryDispatcher _queryDispatcher;

        /// <summary>
        /// Initializes a new instance of the <see cref="StockQuoteService"/> class.
        /// </summary>
        /// <param name="queryDispatcher">The query dispatcher.</param>
        public StockQuoteService(IQueryDispatcher queryDispatcher)
        {
            _queryDispatcher = queryDispatcher;
        }

        /// <summary>
        /// Returns the quote of the given stock by <paramref name="wkn"/>
        /// </summary>
        /// <param name="wkn">The wkn of the stock</param>
        public decimal GetQuote(string wkn)
        {
            var stock = _queryDispatcher.Execute(new StockByWknQuery(wkn));

            if (stock == null || !stock.Quotations.Any())
                return default(decimal);

            return stock.Quotations.Any() ? stock.Quotations.Aggregate((i1, i2) => i1.Date > i2.Date ? i1 : i2).Close : default(decimal);
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