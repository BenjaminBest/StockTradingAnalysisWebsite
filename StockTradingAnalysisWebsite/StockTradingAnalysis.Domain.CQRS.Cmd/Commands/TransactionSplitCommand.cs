using System;
using StockTradingAnalysis.Interfaces.Commands;

namespace StockTradingAnalysis.Domain.CQRS.Cmd.Commands
{
    /// <summary>
    /// The TransactionSplitCommand is used when a split transaction should be executed.
    /// </summary>
    /// <seealso cref="Command" />
    public class TransactionSplitCommand : Command
    {
        /// <summary>
        /// Gets the order date
        /// </summary>
        public DateTime OrderDate { get; private set; }

        /// <summary>
        /// Gets the amount of shares
        /// </summary>
        public decimal Shares { get; private set; }

        /// <summary>
        /// Gets the price per share
        /// </summary>
        public decimal PricePerShare { get; private set; }

        /// <summary>
        /// Gets the underlying stock
        /// </summary>
        public Guid StockId { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionSplitCommand"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="version">The version.</param>
        /// <param name="orderDate">The order date.</param>
        /// <param name="units">The units.</param>
        /// <param name="pricePerUnit">The price per unit.</param>
        /// <param name="stockId">The stock identifier.</param>
        public TransactionSplitCommand(Guid id, int version,
            DateTime orderDate,
            decimal units,
            decimal pricePerUnit,
            Guid stockId)
            : base(version, id)
        {
            OrderDate = orderDate;
            Shares = units;
            PricePerShare = pricePerUnit;
            StockId = stockId;
        }
    }
}