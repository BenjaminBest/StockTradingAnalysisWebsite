using System;
using StockTradingAnalysis.Domain.Events.Events;
using StockTradingAnalysis.EventSourcing.Events;
using StockTradingAnalysis.Interfaces.Events;

namespace StockTradingAnalysis.Domain.Process.Finders
{
    /// <summary>
    /// The TransactionFinder is used to get the correlation id based on supported message types.
    /// </summary>
    public class StatisticsFinder :
        IMessageCorrelationIdCreator<StockQuotationAddedEvent>,
        IMessageCorrelationIdCreator<StockQuotationChangedEvent>,
        IMessageCorrelationIdCreator<StockQuotationsAddedOrChangedEvent>,
        IMessageCorrelationIdCreator<ReplayFinishedEvent>
    {
        /// <summary>
        /// The correlation identifier, in this case static and one for all to re-use the existent process manager related to statistics creation
        /// </summary>
        private static readonly Guid CorrelationId = Guid.NewGuid();

        /// <summary>
        /// Gets the correlation identifier.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        public Guid GetCorrelationId(StockQuotationAddedEvent message)
        {
            return CorrelationId;
        }

        /// <summary>
        /// Gets the correlation identifier.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        public Guid GetCorrelationId(StockQuotationChangedEvent message)
        {
            return CorrelationId;
        }

        /// <summary>
        /// Gets the correlation identifier.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        public Guid GetCorrelationId(StockQuotationsAddedOrChangedEvent message)
        {
            return CorrelationId;
        }

        /// <summary>
        /// Gets the correlation identifier.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        public Guid GetCorrelationId(ReplayFinishedEvent message)
        {
            return CorrelationId;
        }
    }
}
