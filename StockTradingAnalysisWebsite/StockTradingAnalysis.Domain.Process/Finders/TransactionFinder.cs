using System;
using StockTradingAnalysis.Domain.Events.Events;
using StockTradingAnalysis.Interfaces.Events;

namespace StockTradingAnalysis.Domain.Process.Finders
{
    /// <summary>
    /// The TransactionFinder is used to get the correlation id based on supported message types.
    /// </summary>   
    public class TransactionFinder :
        IMessageCorrelationIdCreator<TransactionDividendOrderAddedEvent>,
        IMessageCorrelationIdCreator<TransactionSellingOrderAddedEvent>
    {
        /// <summary>
        /// Gets the correlation identifier.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        public Guid GetCorrelationId(TransactionDividendOrderAddedEvent message)
        {
            return message.Id;
        }

        /// <summary>
        /// Gets the correlation identifier.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        public Guid GetCorrelationId(TransactionSellingOrderAddedEvent message)
        {
            return message.Id;
        }
    }
}
