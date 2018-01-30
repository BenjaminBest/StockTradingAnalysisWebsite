using StockTradingAnalysis.Interfaces.Domain;
using StockTradingAnalysis.Interfaces.Queries;

namespace StockTradingAnalysis.Domain.CQRS.Query.Queries
{
    /// <summary>
    /// The query StockByWknQuery is used to return a stock by a given WKN
    /// </summary>
    /// <seealso cref="Interfaces.Queries.IQuery{IStock}" />
    public class StockByWknQuery : IQuery<IStock>
    {
        /// <summary>
        /// Gets the WKN.
        /// </summary>
        /// <value>
        /// The WKN.
        /// </value>
        public string Wkn { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="StockByWknQuery"/> class.
        /// </summary>
        /// <param name="wkn">The WKN.</param>
        public StockByWknQuery(string wkn)
        {
            Wkn = wkn;
        }
    }
}