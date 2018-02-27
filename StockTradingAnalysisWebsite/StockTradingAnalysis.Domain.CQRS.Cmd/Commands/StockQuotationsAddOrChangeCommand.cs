using System;
using System.Collections.Generic;
using StockTradingAnalysis.Interfaces.Commands;
using StockTradingAnalysis.Interfaces.Domain;

namespace StockTradingAnalysis.Domain.CQRS.Cmd.Commands
{
    /// <summary>
    /// The StockQuotationsAddOrChangeCommand is used when quotations should be added for a stock.
    /// </summary>
    /// <seealso cref="Command" />
    public class StockQuotationsAddOrChangeCommand : Command
    {
        /// <summary>
        /// Gets/sets the quotations
        /// </summary>
        public IEnumerable<IQuotation> Quotations { get; set; }

        /// <summary>
        /// Initializs this object with the given values
        /// </summary>
        /// <param name="stockId">The id if the stock</param>
        /// <param name="stockVersion">The version of the stock</param>
        /// <param name="quotations">The quotations</param>
        public StockQuotationsAddOrChangeCommand(Guid stockId, int stockVersion, IEnumerable<IQuotation> quotations)
            : base(stockVersion, stockId)
        {
            Quotations = quotations;
        }
    }
}