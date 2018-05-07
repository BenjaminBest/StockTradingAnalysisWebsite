using StockTradingAnalysis.Interfaces.Domain;
using StockTradingAnalysis.Web.Common.Interfaces;
using StockTradingAnalysis.Web.Models;

namespace StockTradingAnalysis.Web.Common.Formatting.StatisticConverters
{
    /// <summary>
    /// The converter converts a statistic value to a card view model.
    /// </summary>
    /// <seealso cref="IStatisticCardConverter" />
    public class PayOffRatioToCardConverter : IStatisticCardConverter
    {
        /// <summary>
        /// Gets the order.
        /// </summary>
        /// <value>
        /// The order.
        /// </value>
        public int Order => 7;

        /// <summary>
        /// Converts one property of the specified statistic to a card view model.
        /// </summary>
        /// <param name="statistic">The statistic.</param>
        /// <returns></returns>
        public CardViewModel Convert(IStatistic statistic)
        {
            return new CardViewModel
            {
                Style = new StyleTypeBuilder<decimal>(statistic.PayOffRatio)
                    .Default(StyleType.Info)
                    .WhenGreaterOrEqualThan(0, StyleType.Danger)
                    .WhenGreaterOrEqualThan(0.7m, StyleType.Warning)
                    .WhenGreaterOrEqualThan(1.25m, StyleType.Success)
                    .Compile(),
                Header = Resources.ViewTextKpiPayOffRatio,
                Title = $"{statistic.PayOffRatioDescription} ({statistic.PayOffRatio})",
                FilledBackground = true
            };
        }
    }
}