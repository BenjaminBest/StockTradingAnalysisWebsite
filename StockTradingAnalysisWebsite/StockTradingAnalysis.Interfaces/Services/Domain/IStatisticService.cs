using System.Collections.Generic;
using StockTradingAnalysis.Interfaces.Domain;

namespace StockTradingAnalysis.Interfaces.Services.Domain
{
    /// <summary>
    /// THe interface IStatisticService defines a service to calculate time range based statistical information for transational data
    /// </summary>
    public interface IStatisticService
    {
        /// <summary>
        /// Starts calculation and uses all transactions
        /// </summary>
        /// <param name="transactions">A list with all transactions which should be analyzed</param>
        IStatistic Calculate(IEnumerable<ITransaction> transactions);
    }
}