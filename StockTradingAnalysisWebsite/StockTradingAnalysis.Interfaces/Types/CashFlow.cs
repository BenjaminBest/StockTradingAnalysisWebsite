using System;

namespace StockTradingAnalysis.Interfaces.Types
{
    /// <summary>
    /// The class CashFlow defines a cash flow which consists of amount of money at a particular time
    /// </summary>
    public class CashFlow
    {
        /// <summary>
        /// Gets the date.
        /// </summary>
        /// <value>
        /// The date.
        /// </value>
        public DateTime Date { get; }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public decimal Value { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CashFlow" /> class.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="date">The date.</param>
        public CashFlow(decimal value, DateTime date)
        {
            Date = date;
            Value = value;
        }
    }
}
