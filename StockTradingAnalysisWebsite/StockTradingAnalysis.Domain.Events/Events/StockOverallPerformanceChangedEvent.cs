using System;
using StockTradingAnalysis.Interfaces.Events;

namespace StockTradingAnalysis.Domain.Events.Events
{
    /// <summary>
    /// The event StockOverallPerformanceChangedEvent is fired when the overall performance of a stock was changed.
    /// </summary>
    /// <seealso cref="StockTradingAnalysis.Interfaces.Events.DomainEvent" />
    public class StockOverallPerformanceChangedEvent : DomainEvent
    {
        /// <summary>
        /// Gets the new profit that was made with transactions for this stock
        /// </summary>
        /// <value>
        /// The new profit absolute.
        /// </value>
        public decimal NewProfitAbsolute { get; private set; }

        /// <summary>
        /// Gets the stock identifier.
        /// </summary>
        /// <value>
        /// The stock identifier.
        /// </value>
        public Guid StockId { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="StockOverallPerformanceChangedEvent"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="aggregateType">Type of the aggregate.</param>
        /// <param name="newProfitAbsolute">The new profit absolute.</param>
        /// <param name="stockId">The stock identifier.</param>
        public StockOverallPerformanceChangedEvent(Guid id, Type aggregateType, decimal newProfitAbsolute, Guid stockId)
            : base(id, aggregateType)
        {
            NewProfitAbsolute = newProfitAbsolute;
            StockId = stockId;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StockOverallPerformanceChangedEvent"/> class.
        /// </summary>
        protected StockOverallPerformanceChangedEvent()
        {

        }
    }
}