﻿using StockTradingAnalysis.Interfaces.Domain;
using StockTradingAnalysis.Web.Common.Interfaces;
using StockTradingAnalysis.Web.Models;

namespace StockTradingAnalysis.Web.Common.Formatting.StatisticConverters
{
    /// <summary>
    /// The converter converts a statistic value to a card view model.
    /// </summary>
    /// <seealso cref="IStatisticCardConverter" />
    public class AmountOfLossTransactionsToCardConverter : IStatisticCardConverter
    {
        /// <summary>
        /// Gets the order.
        /// </summary>
        /// <value>
        /// The order.
        /// </value>
        public int Order => 2;

        /// <summary>
        /// Converts one property of the specified statistic to a card view model.
        /// </summary>
        /// <param name="statistic">The statistic.</param>
        /// <returns></returns>
        public CardViewModel Convert(IStatistic statistic)
        {
            return new CardViewModel
            {
                Style = StyleType.Danger,
                Header = Resources.ViewTextKpiAmountOfLossTrades,
                Title = $"{statistic.AmountOfLossTransactions} ({statistic.PercentageOfLossTransactions} %)"
            };
        }
    }
}