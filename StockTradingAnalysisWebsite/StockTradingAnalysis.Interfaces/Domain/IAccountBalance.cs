using System;
using StockTradingAnalysis.Interfaces.ReadModel;

namespace StockTradingAnalysis.Interfaces.Domain
{
    /// <summary>
    /// The interface IAccountBalance describes a single balance for an account
    /// </summary>
    public interface IAccountBalance : IModelRepositoryItem
    {
        /// <summary>
        /// Gets the id of a transaction that changed the balance
        /// </summary>
        Guid TransactionId { get; }

        /// <summary>
        /// Gets the new balance
        /// </summary>
        decimal Balance { get; }

        /// <summary>
        /// Gets the date & time of this balance
        /// </summary>
        DateTime Date { get; }

        /// <summary>
        /// Gets the change.
        /// </summary>
        /// <value>
        /// The change.
        /// </value>
        decimal BalanceChange { get; }
    }
}