﻿using StockTradingAnalysis.Interfaces.Domain;
using StockTradingAnalysis.Web.Common.Interfaces;
using StockTradingAnalysis.Web.Models;

namespace StockTradingAnalysis.Web.Common.Formatting.StatisticConverters
{
    /// <summary>
    /// The converter converts a statistic value to a card view model.
    /// </summary>
    /// <seealso cref="IStatisticCardConverter" />
    public class SqnToCardConverter : IStatisticCardConverter
    {
        /// <summary>
        /// Gets the order.
        /// </summary>
        /// <value>
        /// The order.
        /// </value>
        public int Order => 6;

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
                    .Default(StyleType.Dark)
                    .WhenSmallerThan(0, StyleType.Danger)
                    .WhenSmallerThan(2.5m, StyleType.Warning)
                    .WhenGreaterOrEqualThan(2.5m, StyleType.Success)
                    .Compile(),
                Header = Resources.ViewTextKpiSQN,
                Title = $"{statistic.SqnDescription} ({statistic.Sqn:0.0})",
                FilledBackground = true
            };
        }
    }
}