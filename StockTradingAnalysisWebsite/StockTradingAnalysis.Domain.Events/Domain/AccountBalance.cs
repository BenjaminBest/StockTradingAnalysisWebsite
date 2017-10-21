using System;
using StockTradingAnalysis.Interfaces.Domain;

namespace StockTradingAnalysis.Domain.Events.Domain
{
    /// <summary>
    /// The AccountBalance defines a single balance in point and time.
    /// </summary>
    public class AccountBalance : IAccountBalance
    {
        /// <summary>
        /// Gets the id of a transaction that changed the balance
        /// </summary>
        public Guid TransactionId { get; set; }

        /// <summary>
        /// Gets the new balance
        /// </summary>
        public decimal Balance { get; set; }

        /// <summary>
        /// Gets the date & time of this balance
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets/sets the id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Initializes this object
        /// </summary>
        /// <param name="transactionId">The id of the transaction</param>
        /// <param name="balance">The balance</param>
        /// <param name="date">The date</param>
        public AccountBalance(Guid transactionId, decimal balance, DateTime date)
        {
            TransactionId = transactionId;
            Balance = balance;
            Date = date;
            Id = transactionId;
        }
    }
}