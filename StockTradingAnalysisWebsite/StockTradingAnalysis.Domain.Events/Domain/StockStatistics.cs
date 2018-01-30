using System;
using StockTradingAnalysis.Interfaces.Domain;

namespace StockTradingAnalysis.Domain.Events.Domain
{
    /// <summary>
    /// The StockStatistics defines statistics for one stock
    /// </summary>
    public class StockStatistics : IStockStatistics
    {
        /// <summary>
        /// Gets the new performance
        /// </summary>
        public decimal Performance { get; set; }

        /// <summary>
        /// Gets/sets the id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Initializes this object
        /// </summary>
        /// <param name="stockId">The stock identifier.</param>
        /// <param name="performance">The performance.</param>
        public StockStatistics(Guid stockId, decimal performance)
        {
            Performance = performance;
            Id = stockId;
        }
    }
}