using System;

namespace StockTradingAnalysis.Interfaces.Domain
{
    /// <summary>
    /// Defines an interface for a bucket entry of the transaction book
    /// </summary>
    public interface ITransactionBookEntry
    {
        /// <summary>
        /// Gets the if of a stock
        /// </summary>
        Guid StockId { get; }

        /// <summary>
        /// Gets the id of a transaction
        /// </summary>
        Guid TransactionId { get; }

        /// <summary>
        /// Gets the order date
        /// </summary>
        DateTime OrderDate { get; }

        /// <summary>
        /// Gets/sets the amount of shares
        /// </summary>
        decimal Shares { get; set; }

        /// <summary>
        /// Gets the price per share
        /// </summary>
        decimal PricePerShare { get; }

        /// <summary>
        /// Gets the order costs
        /// </summary>
        decimal OrderCosts { get; set; }

        /// <summary>
        /// Creates a copy of this instance
        /// </summary>
        /// <returns>Copy of this instance</returns>
        ITransactionBookEntry Copy();
    }
}