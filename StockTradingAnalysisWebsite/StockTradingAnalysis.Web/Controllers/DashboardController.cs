using StockTradingAnalysis.Domain.CQRS.Query.Queries;
using StockTradingAnalysis.Interfaces.Queries;
using StockTradingAnalysis.Interfaces.Services;
using StockTradingAnalysis.Web.Common.Filter;
using StockTradingAnalysis.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace StockTradingAnalysis.Web.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IQueryDispatcher _queryDispatcher;
        private readonly IAccumulationPlanStatisticService _accumulationPlanStatisticService;
        private readonly IDateCalculationService _dateCalculationService;
        private readonly IMathCalculatorService _mathCalculatorService;

        public DashboardController(
            IQueryDispatcher queryDispatcher,
            IAccumulationPlanStatisticService accumulationPlanStatisticService,
            IDateCalculationService dateCalculationService,
            IMathCalculatorService mathCalculatorService)
        {
            _queryDispatcher = queryDispatcher;
            _accumulationPlanStatisticService = accumulationPlanStatisticService;
            _dateCalculationService = dateCalculationService;
            _mathCalculatorService = mathCalculatorService;
        }

        // GET: Dashboard
        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Dashboard/Tags
        public ViewResult Tags()
        {
            var model = new List<TagsViewModel>();

            foreach (var tag in _queryDispatcher.Execute(new TransactionTagAllQuery()))
            {
                if (string.IsNullOrEmpty(tag))
                    continue;

                var modelTag = new TagsViewModel() { Tag = tag, Date = DateTime.Now };

                var yearQuery = new TransactionYearsQuery()
                    .Register(new TransactionStartDateFilter(DateTime.MinValue))
                    .Register(new TransactionEndDateFilter(_dateCalculationService.GetEndOfToDay()))
                    .Register(new TransactionTagFilter(tag))
                    .Register(new TransactionDividendFilter(true));

                var years = _queryDispatcher.Execute(yearQuery).ToList();

                foreach (var year in years)
                {
                    var period = new TagPeriodViewModel() { Year = year.ToString() };

                    //Time period
                    DateTime end;
                    var start = _dateCalculationService.GetStartAndEndDateOfYear(new DateTime(year, 1, 1), out end);

                    //Only this year
                    var actualPeriodTransactionsQuery = new TransactionAllQuery()
                        .Register(new TransactionStartDateFilter(start))
                        .Register(new TransactionEndDateFilter(end))
                        .Register(new TransactionTagFilter(tag))
                        .Register(new TransactionDividendFilter(true));

                    var actualPeriodResult = _accumulationPlanStatisticService.Calculate(_queryDispatcher.Execute(actualPeriodTransactionsQuery));

                    period.PerformanceActualPeriodPercentage = actualPeriodResult.PerformancePercentageIIR;
                    period.PerformanceActualPeriodPercentageTW = actualPeriodResult.PerformancePercentageGeometrical;
                    period.Dividends = actualPeriodResult.SumDividends;

                    //Accumulated till this end of year
                    var periodTransactionsQuery = new TransactionAllQuery()
                        .Register(new TransactionStartDateFilter(DateTime.MinValue))
                        .Register(new TransactionEndDateFilter(end))
                        .Register(new TransactionTagFilter(tag))
                        .Register(new TransactionDividendFilter(true));

                    var periodResult = _accumulationPlanStatisticService.Calculate(_queryDispatcher.Execute(periodTransactionsQuery));

                    period.SumOrderCostsPercentage = periodResult.SumOrderCostsPercentage;
                    period.SumOrderCosts = periodResult.SumOrderCosts;
                    period.SumInpayment = periodResult.SumInpayment;
                    period.SumCapital = periodResult.SumCapitalEndofPeriod;
                    period.PerformanceOverallPeriodPercentage = periodResult.PerformancePercentageIIR;
                    period.PerformanceOverallPeriodPercentageTW = periodResult.PerformancePercentageGeometrical;

                    period.DividendsPercentage = decimal.Round((period.Dividends / period.SumInpayment) * 100, 2);

                    modelTag.Periods.Add(period);
                }

                //Forecast

                var startYear = years.Max() + 1;
                foreach (TagPeriodViewModel forecast in GetTagPeriodForecast(2047 - startYear, startYear, modelTag.Periods))
                    modelTag.Periods.Add(forecast);


                model.Add(modelTag);
            }

            return View(model);
        }

        /// <summary>
        /// Calculates a forecast for the given years
        /// </summary>
        /// <param name="startYear">Year to start with</param>
        /// <param name="years">Amount of years</param>
        /// <param name="historical">Historical data</param>
        /// <returns>Forecast</returns>
        private IEnumerable<TagPeriodViewModel> GetTagPeriodForecast(int years, int startYear, IEnumerable<TagPeriodViewModel> historical)
        {
            var periods = new List<TagPeriodViewModel>();

            var tempForecast = new List<TagPeriodViewModel>();
            tempForecast.InsertRange(0, historical.OrderByDescending(t => t.Year));

            var actualYear = startYear;
            while (actualYear <= startYear + years)
            {
                var lastYear = (actualYear - 1).ToString();
                var period = new TagPeriodViewModel() { Year = actualYear.ToString() };

                var lastYearCapital = default(decimal);
                var lastYearInPayment = default(decimal);
                var lastYearOrderCosts = default(decimal);

                var lastYearCalculation = tempForecast.FirstOrDefault(t => t.Year == lastYear);

                if (lastYearCalculation.Year == DateTime.Now.Year.ToString())
                {
                    lastYearCapital = lastYearCalculation.SumCapital + (((lastYearCalculation.SumCapital) / (((tempForecast.Count - 1) * 12) + DateTime.Now.Month)) * (12 - DateTime.Now.Month));
                    lastYearInPayment = lastYearCalculation.SumInpayment + (((lastYearCalculation.SumInpayment) / (((tempForecast.Count - 1) * 12) + DateTime.Now.Month)) * (12 - DateTime.Now.Month));
                    lastYearOrderCosts = lastYearCalculation.SumOrderCosts + (((lastYearCalculation.SumOrderCosts) / (((tempForecast.Count - 1) * 12) + DateTime.Now.Month)) * (12 - DateTime.Now.Month));
                }
                else
                {
                    lastYearCapital = (lastYearCalculation.SumCapital);
                    lastYearInPayment = (lastYearCalculation.SumInpayment);
                    lastYearOrderCosts = (lastYearCalculation.SumOrderCosts);
                }

                period.PerformanceOverallPeriodPercentage = _mathCalculatorService.CalculateGeometricMean(tempForecast.Select(t => t.PerformanceOverallPeriodPercentage));
                period.PerformanceActualPeriodPercentage = _mathCalculatorService.CalculateGeometricMean(tempForecast.Select(t => t.PerformanceOverallPeriodPercentage));
                period.PerformanceOverallPeriodPercentageTW = _mathCalculatorService.CalculateGeometricMean(tempForecast.Select(t => t.PerformanceOverallPeriodPercentageTW));
                period.PerformanceActualPeriodPercentageTW = _mathCalculatorService.CalculateGeometricMean(tempForecast.Select(t => t.PerformanceOverallPeriodPercentageTW));

                var converted = period.PerformanceOverallPeriodPercentageTW / 100;
                var percentagePerfActual = converted < 0 ? 1 - (converted * -1) : 1 + converted;

                period.SumInpayment = (lastYearInPayment / tempForecast.Count) + lastYearInPayment;
                period.SumCapital = (lastYearCapital * percentagePerfActual) + ((lastYearInPayment / tempForecast.Count) * percentagePerfActual);
                period.SumOrderCosts = (lastYearOrderCosts / tempForecast.Count) + lastYearOrderCosts;
                period.SumOrderCostsPercentage = decimal.Round((period.SumOrderCosts / (period.SumOrderCosts - period.SumInpayment) * 100), 2);
                period.IsForecast = true;
                //TODO:period.SumDividends

                periods.Add(period);
                tempForecast.Add(period);
                actualYear++;
            }

            return periods;
        }
    }
}