using System;
using System.Collections.Generic;
using System.Linq;
using StockTradingAnalysis.Domain.CQRS.Query.Filter;
using StockTradingAnalysis.Domain.CQRS.Query.Queries;
using StockTradingAnalysis.Interfaces.Domain;
using StockTradingAnalysis.Interfaces.Queries;
using StockTradingAnalysis.Interfaces.Services.Core;
using StockTradingAnalysis.Interfaces.Services.Domain;
using StockTradingAnalysis.Interfaces.Types;
using StockTradingAnalysis.Services.Domain;

namespace StockTradingAnalysis.Services.Services
{
    /// <summary>
    /// The class AccumulationPlanStatisticService defines a service to calculate time range based statistical information for saving plans
    /// </summary>
    public class AccumulationPlanStatisticService : IAccumulationPlanStatisticService
    {
        /// <summary>
        /// The date calculation service
        /// </summary>
        private readonly IDateCalculationService _dateCalculationService;

        /// <summary>
        /// The query dispatcher
        /// </summary>
        private readonly IQueryDispatcher _queryDispatcher;

        /// <summary>
        /// The iir calculation service
        /// </summary>
        private readonly IInterestRateCalculatorService _iirCalculationService;

        /// <summary>
        /// The transaction calculation service
        /// </summary>
        private readonly ITransactionCalculationService _transactionCalculationService;

        /// <summary>
        /// Initializes this service with the given values
        /// </summary>
        /// <param name="dateCalculationService">The date calculation service</param>
        /// <param name="queryDispatcher">The query dispatcher to retrieve stock quotes</param>
        /// <param name="iirCalculationService">The iir calculation service.</param>
        /// <param name="transactionCalculationService">The transaction calculation service.</param>
        public AccumulationPlanStatisticService(
            IDateCalculationService dateCalculationService,
            IQueryDispatcher queryDispatcher,
            IInterestRateCalculatorService iirCalculationService,
            ITransactionCalculationService transactionCalculationService)
        {
            _dateCalculationService = dateCalculationService;
            _queryDispatcher = queryDispatcher;
            _iirCalculationService = iirCalculationService;
            _transactionCalculationService = transactionCalculationService;
        }

        /// <summary>
        /// Calculates the savings plan for the given <paramref name="tag"/>.
        /// </summary>
        /// <param name="tag">The tag.</param>
        public ISavingsPlan CalculateSavingsPlan(string tag)
        {
            var savingsPlan = new SavingsPlan { Tag = tag, Date = DateTime.Now };

            var yearQuery = new TransactionYearsQuery()
                .Register(new TransactionStartDateFilter(DateTime.MinValue))
                .Register(new TransactionEndDateFilter(_dateCalculationService.GetEndOfToDay()))
                .Register(new TransactionTagFilter(tag))
                .Register(new TransactionDividendFilter(true));

            var years = _queryDispatcher.Execute(yearQuery).ToList();

            foreach (var year in years)
            {
                var period = new SavingsPlanPeriod() { Year = year.ToString() };

                //Time period
                var start = _dateCalculationService.GetStartAndEndDateOfYear(new DateTime(year, 1, 1), out DateTime end);
                var lastYearEnd = _dateCalculationService.GetEndDateOfYear(new DateTime(years[0] - 1, 1, 1));

                //Only this year
                var actualPeriodResult = CalculateCurrentPeriod(start, end, tag);

                period.PerformanceActualPeriodPercentage = actualPeriodResult.PerformanceActualPeriodPercentage;
                period.SumDividends = actualPeriodResult.SumDividends;

                //Accumulated till this end of year                
                var periodResult = CalculateOverallPeriod(lastYearEnd, end, tag);

                period.SumOrderCostsPercentage = periodResult.SumOrderCostsPercentage;
                period.SumOrderCosts = periodResult.SumOrderCosts;
                period.SumInpayment = periodResult.SumInpayment;
                period.SumCapital = periodResult.SumCapital;
                period.PerformanceOverallPeriodPercentage = periodResult.PerformanceOverallPeriodPercentage;
                period.SumOverallDividends = periodResult.SumOverallDividends;
                period.SumOverallDividendsPercentage = decimal.Round((periodResult.SumOverallDividends / periodResult.SumInpayment) * 100, 2);
                period.IsCurrentYear = DateTime.Now.Year == year;
                period.IsForecast = false;

                savingsPlan.Periods.Add(period);
            }

            //Forecast
            if (savingsPlan.Periods.Any())
            {
                var startYear = years.Max() + 1;
                var amountOfForecastYears = years.Min() + 35 - startYear;
                foreach (ISavingsPlanPeriod forecast in GetTagPeriodForecast(amountOfForecastYears, startYear, savingsPlan.Periods))
                    savingsPlan.Periods.Add(forecast);
            }

            return savingsPlan;
        }

        /// <summary>
        /// Calculate current saving for the given period
        /// </summary>
        /// <param name="start">The start.</param>
        /// <param name="end">The end.</param>
        /// <param name="tag">The tag.</param>
        /// <returns></returns>
        internal ISavingsPlanPeriodCurrent CalculateCurrentPeriod(DateTime start, DateTime end, string tag)
        {
            var query = new TransactionAllQuery()
                .Register(new TransactionStartDateFilter(start))
                .Register(new TransactionEndDateFilter(end))
                .Register(new TransactionTagFilter(tag));

            var period = new SavingsPlanPeriod
            {
                SumDividends = _transactionCalculationService.CalculateSumDividends(query),
                PerformanceActualPeriodPercentage =
                    _transactionCalculationService.CalculatePerformancePercentageForPeriod(
                        new TransactionAllQuery().Register(new TransactionTagFilter(tag)), start, end)
            };

            return period;
        }

        /// <summary>
        /// Caculate overall savings for the given period
        /// </summary>
        /// <param name="start">The start.</param>
        /// <param name="end">The end.</param>
        /// <param name="tag">The tag.</param>
        /// <returns></returns>
        internal ISavingsPlanPeriodOverall CalculateOverallPeriod(DateTime start, DateTime end, string tag)
        {
            var query = new TransactionAllQuery()
                .Register(new TransactionStartDateFilter(start))
                .Register(new TransactionEndDateFilter(end))
                .Register(new TransactionTagFilter(tag));

            var transactions = _queryDispatcher.Execute(query).ToList();

            var period = new SavingsPlanPeriod { IsForecast = false };

            if (!transactions.Any())
                return period;

            //Inpayments
            period.SumInpayment = _transactionCalculationService.CalculateSumInpayments(query);

            //Order costs
            period.SumOrderCosts -= transactions.Sum(t => t.OrderCosts);
            period.SumOrderCostsPercentage = decimal.Round((period.SumOrderCosts / (period.SumOrderCosts - period.SumInpayment) * 100), 2);

            //Sum capital
            period.SumCapital = _transactionCalculationService.CalculateSumCapital(query, end);

            //Dividends
            period.SumOverallDividends = _transactionCalculationService.CalculateSumDividends(query);

            //PerformancePercentage
            var cashFlowEnd = new CashFlow(period.SumCapital, end);
            period.PerformanceOverallPeriodPercentage = _transactionCalculationService.CalculatePerformancePercentageIir(query, new CashFlow(0, start), cashFlowEnd);

            return period;
        }

        /// <summary>
        /// Calculates a forecast for the given years
        /// </summary>
        /// <param name="startYear">Year to start with</param>
        /// <param name="years">Amount of years</param>
        /// <param name="historical">Historical data</param>
        /// <returns>Forecast</returns>
        private IEnumerable<ISavingsPlanPeriod> GetTagPeriodForecast(int years, int startYear, IEnumerable<ISavingsPlanPeriod> historical)
        {
            if (historical == null)
                return Enumerable.Empty<ISavingsPlanPeriod>();

            var periods = new List<ISavingsPlanPeriod>();
            var historicalData = historical.OrderByDescending(t => t.Year).ToList();

            var tempForecast = new List<ISavingsPlanPeriod>();
            tempForecast.InsertRange(0, historicalData);

            var actualYear = startYear;
            while (actualYear <= startYear + years)
            {
                var lastYear = (actualYear - 1).ToString();
                var period = new SavingsPlanPeriod() { Year = actualYear.ToString() };

                decimal lastYearCapital;
                decimal lastYearInPayment;
                decimal lastYearOrderCosts;
                decimal lastYearSumOverallDividends;

                var lastYearCalculation = tempForecast.FirstOrDefault(t => t.Year == lastYear);

                if (lastYearCalculation.Year == DateTime.Now.Year.ToString())
                {
                    lastYearCapital = lastYearCalculation.SumCapital + ((lastYearCalculation.SumCapital / (((tempForecast.Count - 1) * 12) + DateTime.Now.Month)) * (12 - DateTime.Now.Month));
                    lastYearInPayment = lastYearCalculation.SumInpayment + ((lastYearCalculation.SumInpayment / (((tempForecast.Count - 1) * 12) + DateTime.Now.Month)) * (12 - DateTime.Now.Month));
                    lastYearOrderCosts = lastYearCalculation.SumOrderCosts + ((lastYearCalculation.SumOrderCosts / (((tempForecast.Count - 1) * 12) + DateTime.Now.Month)) * (12 - DateTime.Now.Month));
                    lastYearSumOverallDividends = lastYearCalculation.SumOverallDividends + ((lastYearCalculation.SumOverallDividends / (((tempForecast.Count - 1) * 12) + DateTime.Now.Month)) * (12 - DateTime.Now.Month));
                }
                else
                {
                    lastYearCapital = lastYearCalculation.SumCapital;
                    lastYearInPayment = lastYearCalculation.SumInpayment;
                    lastYearOrderCosts = lastYearCalculation.SumOrderCosts;
                    lastYearSumOverallDividends = lastYearCalculation.SumOverallDividends;
                }

                period.PerformanceOverallPeriodPercentage = historicalData.FirstOrDefault().PerformanceOverallPeriodPercentage;
                period.PerformanceActualPeriodPercentage = period.PerformanceOverallPeriodPercentage;

                var converted = period.PerformanceOverallPeriodPercentage / 100;
                var percentagePerfActual = converted < 0 ? 1 - (converted * -1) : 1 + converted;

                period.SumInpayment = (lastYearInPayment / tempForecast.Count) + lastYearInPayment;
                period.SumCapital = (lastYearCapital * percentagePerfActual) + ((lastYearInPayment / tempForecast.Count) * percentagePerfActual);
                period.SumOrderCosts = (lastYearOrderCosts / tempForecast.Count) + lastYearOrderCosts;
                period.SumOrderCostsPercentage = decimal.Round((period.SumOrderCosts / (period.SumOrderCosts - period.SumInpayment) * 100), 2);

                period.SumOverallDividendsPercentage = historicalData.FirstOrDefault().SumOverallDividendsPercentage;
                period.SumOverallDividends = decimal.Round((period.SumOverallDividendsPercentage / 100) * period.SumInpayment, 2) + lastYearSumOverallDividends;
                period.SumDividends = 0;

                period.IsForecast = true;

                periods.Add(period);
                tempForecast.Add(period);
                actualYear++;
            }

            return periods;
        }
    }
}
