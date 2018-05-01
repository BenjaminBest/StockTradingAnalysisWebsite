using StockTradingAnalysis.Interfaces.Domain;
using StockTradingAnalysis.Web.Models;

namespace StockTradingAnalysis.Web.Common.Interfaces
{
    /// <summary>
    /// The interface IStatisticCardConverter defines a converter which maps one statistic property to a card viewmodel.
    /// </summary>
    public interface IStatisticCardConverter
    {
        /// <summary>
        /// Gets the order.
        /// </summary>
        /// <value>
        /// The order.
        /// </value>
        int Order { get; }

        /// <summary>
        /// Converts one property of the specified statistic to a card view model.
        /// </summary>
        /// <param name="statistic">The statistic.</param>
        /// <returns></returns>
        CardViewModel Convert(IStatistic statistic);
    }
}