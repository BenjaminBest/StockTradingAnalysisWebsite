﻿using System;
using System.Collections.Generic;
using System.Linq;
using StockTradingAnalysis.Interfaces.Domain;
using StockTradingAnalysis.Interfaces.Services.Domain;
using StockTradingAnalysis.Services.Domain;

namespace StockTradingAnalysis.Services.Services
{
    /// <summary>
    /// The class StatisticService defines a service to calculate time range based statistical information for transational data
    /// </summary>
    public class StatisticService : IStatisticService
    {
        /// <summary>
        /// Starts calculation and uses all transactions
        /// </summary>
        /// <param name="transactions">A list with all transactions which should be analyzed</param>
        public IStatistic Calculate(IEnumerable<ITransaction> transactions)
        {
            if (transactions == null)
                return null;

            if (!transactions.Any())
                return null;

            return new Statistic(DateTime.MinValue, DateTime.MaxValue); //TODO: Calculate, but put every calculation in a seperate class!
        }
    }
}
