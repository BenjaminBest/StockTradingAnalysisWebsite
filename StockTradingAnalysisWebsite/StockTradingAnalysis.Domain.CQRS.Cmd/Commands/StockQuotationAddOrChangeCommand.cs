using StockTradingAnalysis.Interfaces.Commands;
using StockTradingAnalysis.Interfaces.Domain;
using System;

namespace StockTradingAnalysis.Domain.CQRS.Cmd.Commands
{
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