using System;

namespace StockTradingAnalysis.Interfaces.Types
{
    /// <summary>
    /// The class CashFlow defines a cash flow which consists of amount of money at a particular time
    /// </summary>
    public class CashFlow
    {
        /// <summary>
        /// Amount of money
        /// </summary>
        public readonly decimal Amount;

        /// <summary>
        /// Particular time
        /// </summary>
        public readonly DateTime Date;

        /// <summary>
        /// Initializes this object with the given values
        /// </summary>
        /// <param name="amount">Amount of money</param>
        /// <param name="date">Particular time</param>
        public CashFlow(decimal amount, DateTime date)
        {
            Amount = amount;
            Date = date;
        }
    }
}
