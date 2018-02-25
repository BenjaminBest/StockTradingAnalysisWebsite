using System;
using StockTradingAnalysis.Domain.CQRS.Cmd.Commands;
using StockTradingAnalysis.Interfaces.Events;

namespace StockTradingAnalysis.Domain.Process.Finders
{
    /// <summary>
    /// The TransactionFinder is used to get the correlation id based on supported message types.
    /// </summary>
    /// <seealso cref="Interfaces.Events.IMessageCorrelationIdCreator{TransactionDividendCommand}" />
    /// <seealso cref="Interfaces.Events.IMessageCorrelationIdCreator{TransactionSellCommand}" />
    public class TransactionFinder :
        IMessageCorrelationIdCreator<TransactionDividendCommand>,
        IMessageCorrelationIdCreator<TransactionSellCommand>
    {
        /// <summary>
        /// Gets the correlation identifier.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        public Guid GetCorrelationId(TransactionDividendCommand message)
        {
            return message.Id;
        }

        /// <summary>
        /// Gets the correlation identifier.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        public Guid GetCorrelationId(TransactionSellCommand message)
        {
            return message.Id;
        }
    }
}
