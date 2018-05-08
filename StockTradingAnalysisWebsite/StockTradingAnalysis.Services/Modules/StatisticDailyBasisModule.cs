using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using StockTradingAnalysis.Core.Extensions;
using StockTradingAnalysis.Interfaces.Domain;
using StockTradingAnalysis.Services.Domain;

namespace StockTradingAnalysis.Services.Modules
{
    /// <summary>
    /// The StatisticDailyBasisModule is a calculation module for the statistic service to calculate values on a daily basis
    /// </summary>
    public static class StatisticDailyBasisModule
    {
        /// <summary>
        /// Calculates the costs.
        /// </summary>
        /// <param name="statistic">The statistic.</param>
        /// <param name="transactions">The transactions.</param>
        /// <param name="transactionPerformances">The transaction performances.</param>
        public static void CalculateDailyBasis(this Statistic statistic,
            IReadOnlyList<ITransaction> transactions,
            IReadOnlyList<ITransactionPerformance> transactionPerformances)
        {
            var transactionHistory =
                from t in transactions.OfTypes<ITransaction, ISellingTransaction, IDividendTransaction>()
                join tp in transactionPerformances on t.Id equals tp.Id
                orderby t.OrderDate
                select new { OrderDate = t.OrderDate.Date, Performance = tp };

            foreach (var dayName in CultureInfo.CurrentCulture.DateTimeFormat.DayNames)
            {
                statistic.AbsoluteProfitPerWeekDay.Add(dayName, 0);
            }

            var lastLoss = 0; var lastWin = 0; var maxDrawdown = 0.0m;

            foreach (var information in transactionHistory)
            {
                //TODO: MAEDistribution,MFEDistribution

                //MostConsecutive
                if (!information.Performance.ProfitMade)
                {
                    maxDrawdown = maxDrawdown + information.Performance.ProfitAbsolute;
                    lastLoss = lastLoss + 1; lastWin = 0;
                    statistic.MostConsecutiveLosses = lastLoss > statistic.MostConsecutiveLosses ? lastLoss : statistic.MostConsecutiveLosses;
                }
                if (information.Performance.ProfitMade)
                {
                    maxDrawdown = 0;
                    lastWin = lastWin + 1; lastLoss = 0;
                    statistic.MostConsecutiveWins = lastWin > statistic.MostConsecutiveWins ? lastWin : statistic.MostConsecutiveWins;
                }

                if (maxDrawdown < statistic.MaxDrawdown)
                    statistic.MaxDrawdown = maxDrawdown;

                //Profit per Weekday
                var weekday = CultureInfo.CurrentCulture.DateTimeFormat.DayNames[(int)information.OrderDate.DayOfWeek];
                statistic.AbsoluteProfitPerWeekDay[weekday] += information.Performance.ProfitAbsolute;
            }
        }
    }
}
