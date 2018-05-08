using System;
using System.Collections.Generic;
using System.Linq;
using StockTradingAnalysis.Core.Extensions;
using StockTradingAnalysis.Interfaces.Domain;
using StockTradingAnalysis.Interfaces.Services.Core;
using StockTradingAnalysis.Services.Domain;

namespace StockTradingAnalysis.Services.Modules
{
    /// <summary>
    /// The StatisticEfficiencyAmountModule is a calculation module for the statistic service which calculates efficiencies.
    /// </summary>
    public static class StatisticEfficiencyAmountModule
    {
        /// <summary>
        /// Calculates the costs.
        /// </summary>
        /// <param name="statistic">The statistic.</param>
        /// <param name="transactionPerformances">The transactions performances.</param>
        /// <param name="transactions">The original transactions.</param>
        /// <param name="mathCalculatorService">Calculation service for complex mathematical formulars.</param>
        public static void CalculateEfficiency(this Statistic statistic,
            IReadOnlyList<ITransactionPerformance> transactionPerformances,
            IReadOnlyList<ITransaction> transactions,
                IMathCalculatorService mathCalculatorService)
        {
            statistic.AvgEntryEfficiency = mathCalculatorService.CalculateGeometricMean(transactionPerformances
                .Where(t => t.EntryEfficiency.HasValue).Select(t => t.EntryEfficiency.Value));

            statistic.AvgExitEfficiency = mathCalculatorService.CalculateGeometricMean(transactionPerformances
                .Where(t => t.ExitEfficiency.HasValue).Select(t => t.ExitEfficiency.Value));

            statistic.TradeAveragePercentage =
                mathCalculatorService.CalculateGeometricMean(transactionPerformances.Select(t => t.ProfitPercentage));
            statistic.TradeAverage = decimal.Round(transactionPerformances.Average(s => s.ProfitAbsolute), 2);

            transactions.OfType<IBuyingTransaction>().WhenNotNullOrEmpty(t => statistic.AverageCRV = decimal.Round(t.Average(c => c.CRV), 2));

            //Payoff-Ratio
            statistic.PayOffRatio = decimal.Round(statistic.ProfitAverage / (statistic.LossAverage == 0 ? -1 : statistic.LossAverage) * -1, 2);


            //TODO: Use rule engine builder to make it more readable
            if (statistic.PayOffRatio >= 0 && statistic.PayOffRatio < (decimal)0.5)
                statistic.PayOffRatioDescription = Resources.Display_SQN1;
            else if (statistic.PayOffRatio >= (decimal)0.5 && statistic.PayOffRatio < (decimal)0.7)
                statistic.PayOffRatioDescription = Resources.Display_SQN2;
            else if (statistic.PayOffRatio >= (decimal)0.7 && statistic.PayOffRatio < 1)
                statistic.PayOffRatioDescription = Resources.Display_SQN4;
            else if (statistic.PayOffRatio >= 1 && statistic.PayOffRatio < (decimal)1.25)
                statistic.PayOffRatioDescription = Resources.Display_SQN5;
            else if (statistic.PayOffRatio >= (decimal)1.25 && statistic.PayOffRatio < (decimal)1.5)
                statistic.PayOffRatioDescription = Resources.Display_SQN6;
            else if (statistic.PayOffRatio >= (decimal)1.5 && statistic.PayOffRatio < (decimal)2.0)
                statistic.PayOffRatioDescription = Resources.Display_SQN7;
            else if (statistic.PayOffRatio >= (decimal)2.0 && statistic.PayOffRatio < (decimal)3.0)
                statistic.PayOffRatioDescription = Resources.Display_SQN8;
            else if (statistic.PayOffRatio >= (decimal)3.0)
                statistic.PayOffRatioDescription = Resources.Display_SQN9;
            else
                statistic.PayOffRatioDescription = Resources.Display_SQN0;

            //SQN(System quality number)            
            var tradeAmountSqrt = mathCalculatorService.CalculateSquareRoot(transactionPerformances.Count);
            var propability = (double)(transactionPerformances.Sum(t => t.R) / transactionPerformances.Count);
            var stDeviation = mathCalculatorService.CalculateStandardDeviation(transactionPerformances.Select(t => (double)t.R).ToList(), 2);


            if (Math.Abs(stDeviation) > 0)
            {
                statistic.Sqn = decimal.Round((decimal)(tradeAmountSqrt * (propability / stDeviation)), 2);

                //TODO: Use rule engine builder to make it more readable
                if (statistic.Sqn < -1)
                    statistic.SqnDescription = Resources.Display_SQN1;
                else if (statistic.Sqn >= -1 && statistic.Sqn < 0)
                    statistic.SqnDescription = Resources.Display_SQN2;
                else if (statistic.Sqn >= 0 && statistic.Sqn < (decimal)1.6)
                    statistic.SqnDescription = Resources.Display_SQN3;
                else if (statistic.Sqn >= (decimal)1.6 && statistic.Sqn < (decimal)2.0)
                    statistic.SqnDescription = Resources.Display_SQN4;
                else if (statistic.Sqn >= (decimal)2.0 && statistic.Sqn < (decimal)2.5)
                    statistic.SqnDescription = Resources.Display_SQN5;
                else if (statistic.Sqn >= (decimal)2.5 && statistic.Sqn < (decimal)3.0)
                    statistic.SqnDescription = Resources.Display_SQN6;
                else if (statistic.Sqn >= (decimal)3.0 && statistic.Sqn < (decimal)5.0)
                    statistic.SqnDescription = Resources.Display_SQN7;
                else if (statistic.Sqn >= (decimal)5.0 && statistic.Sqn < (decimal)7.0)
                    statistic.SqnDescription = Resources.Display_SQN8;
                else if (statistic.Sqn >= (decimal)7.0)
                    statistic.SqnDescription = Resources.Display_SQN9;
                else
                    statistic.SqnDescription = Resources.Display_SQN0;
            }
        }
    }
}
