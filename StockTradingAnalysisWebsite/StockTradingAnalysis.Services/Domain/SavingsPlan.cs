using System;
using System.Collections.Generic;
using StockTradingAnalysis.Interfaces.Domain;

namespace StockTradingAnalysis.Services.Domain
{
    /// <summary>
    /// The interface ISavingsPlan defines a savingsplan based on tagged transactions
    /// </summary>
    public class SavingsPlan : ISavingsPlan
    {
        /// <summary>
        /// Gets or sets the date.
        /// </summary>
        /// <value>
        /// The date.
        /// </value>
        public DateTime Date { get; set; }

        /// <summary>
        /// Historical information over periods
        /// </summary>
        public IList<ISavingsPlanPeriod> Periods { get; set; }

        /// <summary>
        /// Gets or sets the tag.
        /// </summary>
        /// <value>
        /// The tag.
        /// </value>
        public string Tag { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SavingsPlan"/> class.
        /// </summary>
        public SavingsPlan()
        {
            Periods = new List<ISavingsPlanPeriod>();
        }
    }
}