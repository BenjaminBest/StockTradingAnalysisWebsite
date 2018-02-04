using System;
using System.Collections.Generic;

namespace StockTradingAnalysis.Interfaces.Domain
{
    /// <summary>
    /// The interface ISavingsPlan defines a savingsplan based on tagged transactions
    /// </summary>
    public interface ISavingsPlan
    {
        /// <summary>
        /// Gets or sets the date.
        /// </summary>
        /// <value>
        /// The date.
        /// </value>
        DateTime Date { get; set; }

        /// <summary>
        /// Historical information over periods
        /// </summary>
        IList<ISavingsPlanPeriod> Periods { get; set; }

        /// <summary>
        /// Gets or sets the tag.
        /// </summary>
        /// <value>
        /// The tag.
        /// </value>
        string Tag { get; set; }
    }
}