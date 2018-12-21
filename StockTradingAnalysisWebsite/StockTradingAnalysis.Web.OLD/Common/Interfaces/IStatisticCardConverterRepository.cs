using StockTradingAnalysis.Interfaces.Domain;
using StockTradingAnalysis.Web.Models;

namespace StockTradingAnalysis.Web.Common.Interfaces
{
    /// <summary>
    /// The interface IStatisticCardConverterRepository defines a repository which holds all statistic property to card converters.
    /// </summary>
    public interface IStatisticCardConverterRepository
    {
        /// <summary>
        /// Converts the statistic to a cards viewmodel.
        /// </summary>
        /// <param name="statistic">The statistic.</param>
        /// <returns>Cards viewmodel</returns>
        CardsViewModel ConvertStatistic(IStatistic statistic);
    }
}