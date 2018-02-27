using System;
using StockTradingAnalysis.Interfaces.Commands;
using StockTradingAnalysis.Interfaces.Domain;

namespace StockTradingAnalysis.Domain.CQRS.Cmd.Commands
{
    /// <summary>
    /// The StockQuotationAddOrChangeCommand is used when a quotation for a stock has changed or should be added.
    /// </summary>
    /// <seealso cref="Command" />
    public class StockQuotationAddOrChangeCommand : Command
    {
        /// <summary>
        /// Gets/sets the quotation
        /// </summary>
        public IQuotation Quotation { get; set; }

        /// <summary>
        /// Initializs this object with the given values
        /// </summary>
        /// <param name="stockId">The id if the stock</param>
        /// <param name="stockVersion">The version of the stock</param>
        /// <param name="quotation">The quotation</param>
        public StockQuotationAddOrChangeCommand(Guid stockId, int stockVersion, IQuotation quotation)
            : base(stockVersion, stockId)
        {
            Quotation = quotation;
        }
    }
}