using System;
using System.Collections.Generic;

namespace StockTradingAnalysis.Web.Models
{
    /// <summary>
    /// Class SavingsPlanViewModel is used for showing information about tagged transactions
    /// </summary>
    public class SavingsPlanViewModel
    {
        /// <summary>
        /// Tag
        /// </summary>
        public string Tag
        {
            get;
            set;
        }

        /// <summary>
        /// Date
        /// </summary>
        public DateTime Date
        {
            get;
            set;
        }

        /// <summary>
        /// Historical information over periods
        /// </summary>
        public IList<SavingsPlanPeriodViewModel> Periods
        {
            get;
            set;
        }

        /// <summary>
        /// Initializes this object with default values
        /// </summary>
        public SavingsPlanViewModel()
        {
            Periods = new List<SavingsPlanPeriodViewModel>();
        }
    }
}