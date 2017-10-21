using StockTradingAnalysis.Interfaces.Domain;
using System.Collections.Generic;

namespace StockTradingAnalysis.Interfaces.Services
{
    /// <summary>
    /// The interface IAccumulationPlanStatisticService defines a service to calculate time range based statistical information for saving plans
    /// </summary>
    public interface IAccumulationPlanStatisticService
    {
        /// <summary>
        /// Starts calculation and uses all transactions
        /// </summary>
        /// <param name="transactions">A list with all transactions which should be analyzed</param>
        /// <returns>statistical information</returns>
        IAccumulationPlanStatistic Calculate(IEnumerable<ITransaction> transactions);
    }
}