using StockTradingAnalysis.Domain.CQRS.Query.Queries;
using StockTradingAnalysis.Interfaces.Domain;
using StockTradingAnalysis.Interfaces.Queries;
using StockTradingAnalysis.Interfaces.Services;
using StockTradingAnalysis.Interfaces.Types;
using StockTradingAnalysis.Services.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StockTradingAnalysis.Services.Services
{
    /// <summary>
    /// The class AccumulationPlanStatisticService defines a service to calculate time range based statistical information for saving plans
    /// </summary>
    public class AccumulationPlanStatisticService : IAccumulationPlanStatisticService
    {
        private readonly IDateCalculationService _dateCalculationService;
        private readonly IInterestRateCalculatorService _interestRateCalculatorService;
        private readonly IQueryDispatcher _queryDispatcher;

        /// <summary>
        /// Initializes this service with the given values
        /// </summary>
        /// <param name="dateCalculationService">The date calculation service</param>
        /// <param name="interestRateCalculatorService">The interest rate calculation service</param>
        /// <param name="queryDispatcher">The query dispatcher to retrieve stock quotes</param>
        public AccumulationPlanStatisticService(
            IDateCalculationService dateCalculationService,
            IInterestRateCalculatorService interestRateCalculatorService,
            IQueryDispatcher queryDispatcher)
        {
            _dateCalculationService = dateCalculationService;
            _interestRateCalculatorService = interestRateCalculatorService;
            _queryDispatcher = queryDispatcher;
        }

        /// <summary>
        /// Starts calculation and uses all transactions
        /// </summary>
        /// <param name="transactions">A list with all transactions which should be analyzed</param>
        public IAccumulationPlanStatistic Calculate(IEnumerable<ITransaction> transactions)
        {
            if (transactions == null)
                return null;

            var list = transactions as IList<ITransaction> ?? transactions.ToList();
            if (!list.Any())
                return null;

            //Period start and end
            var periodStart = _dateCalculationService.GetStartDateOfYear(list.Min(t => t.OrderDate));
            var periodEnd = _dateCalculationService.GetEndDateOfYear(list.Max(t => t.OrderDate));

            var statistic = new AccumulationPlanStatistic();

            //Inpayment amounts
            statistic.SumInpayment = CalculateSumInpayment(list);

            //Order costs
            statistic.SumOrderCosts -= list.Sum(t => t.OrderCosts);
            statistic.SumOrderCostsPercentage = decimal.Round((statistic.SumOrderCosts / (statistic.SumOrderCosts - statistic.SumInpayment) * 100), 2);

            //Sum capital
            statistic.SumCapitalEndofPeriod = CalculateSumCapital(list, periodStart, periodEnd);
            statistic.SumCapital = CalculateSumCapital(list, periodStart, list.Max(t => t.OrderDate));
            statistic.SumCapitalToday = CalculateSumCapital(list, periodStart, periodEnd);

            //Dividends
            statistic.SumDividends = CalculateSumDividends(list);

            //PerformancePercentageIIR
            statistic.PerformancePercentageIIR = CalculatePerformancePercentageIIR(list, statistic.SumCapitalEndofPeriod, periodEnd);

            //PerformancePercentageGeometrical
            statistic.PerformancePercentageGeometrical = CalculatePerformancePercentageGeometrical(statistic.SumInpayment, statistic.SumCapitalEndofPeriod);

            return statistic;
        }

        /// <summary>
        /// Calculates the sum of dividends
        /// </summary>
        /// <param name="transactions">The list of transactions</param>
        /// <returns>Sum dividends</returns>
        internal decimal CalculateSumDividends(IEnumerable<ITransaction> transactions)
        {
            if (transactions == null)
                return default(decimal);

            return transactions.Where(t => t is IDividendTransaction).Cast<IDividendTransaction>().Sum(tr => tr.PositionSize - tr.OrderCosts - tr.Taxes);
        }

        /// <summary>
        /// Calculates the sum of inpayments
        /// </summary>
        /// <param name="transactions">The list of transactions</param>
        /// <returns>Sum inpayment</returns>
        internal decimal CalculateSumInpayment(IEnumerable<ITransaction> transactions)
        {
            var sum = default(decimal);

            foreach (var tr in transactions.Where(t => !(t is IDividendTransaction)))
            {
                if (tr is ISellingTransaction)
                {
                    sum += (tr.PositionSize) * -1;
                }

                if (tr is IBuyingTransaction)
                {
                    sum += (tr.PositionSize);
                }
            }

            return sum;
        }

        /// <summary>
        /// Calculates the sum of all capital
        /// </summary>
        /// <param name="transactions">The list of transactions</param>
        /// <param name="periodStart">The start date of the period</param>
        /// <param name="periodEnd">The end date of the period</param>
        /// <returns>Sum Capital</returns>
        internal decimal CalculateSumCapital(IEnumerable<ITransaction> transactions, DateTime periodStart, DateTime periodEnd)
        {
            var sumCapital = default(decimal);

            var trans = transactions.ToList();

            var stocks = trans.Where(t => !(t is IDividendTransaction)).GroupBy(t => t.Stock).Select(t => t.Key);

            foreach (var stock in stocks)
            {
                IEnumerable<ITransaction> filtered = trans.Where(t => t.Stock.Id == stock.Id).OrderByDescending(t => t.OrderDate);

                decimal allUnitsOfStock = 0;
                foreach (var tr in filtered)
                {
                    if (tr is IBuyingTransaction)
                    {
                        allUnitsOfStock += tr.Shares;

                    }

                    if (tr is ISellingTransaction)
                    {
                        allUnitsOfStock += tr.Shares * -1;

                    }
                }

                var quotations = _queryDispatcher.Execute(new StockQuotationsByIdQuery(stock.Id)).ToList();

                quotations = quotations?.Where(q => q.Date >= periodStart && q.Date <= periodEnd).OrderByDescending(q => q.Date).ToList();

                if (!quotations.Any())
                {
                    sumCapital += (filtered.FirstOrDefault().PricePerShare * allUnitsOfStock);
                }
                else
                {
                    sumCapital += (quotations.FirstOrDefault().Close * allUnitsOfStock);
                }
            }

            return sumCapital;
        }

        /// <summary>
        /// Calculates the performance with the internal rate of interest
        /// </summary>
        /// <param name="transactions">The list of transactions</param>
        /// <param name="sumCapitalEndofPeriod">Sum of the capital at the end of the period</param>
        /// <param name="periodEnd">The end of the period</param>
        /// <returns>Performance in %</returns>
        internal decimal CalculatePerformancePercentageIIR(IEnumerable<ITransaction> transactions, decimal sumCapitalEndofPeriod, DateTime periodEnd)
        {
            var bigcfs = new List<CashFlow>();

            foreach (var tr in transactions.Where(t => !(t is IDividendTransaction)))
            {
                if (tr is IBuyingTransaction)
                {
                    bigcfs.Add(new CashFlow((tr.PositionSize + tr.OrderCosts) * -1, tr.OrderDate));
                }

                if (tr is ISellingTransaction)
                {
                    bigcfs.Add(new CashFlow(tr.PositionSize + tr.OrderCosts, tr.OrderDate));
                }
            }

            bigcfs.Add(new CashFlow(sumCapitalEndofPeriod, periodEnd));

            var val = _interestRateCalculatorService.Calculate(bigcfs).Value;

            return decimal.Round(Convert.ToDecimal(val) * 100, 2);
        }

        /// <summary>
        /// Calculates the performance with normal time weighted method
        /// </summary>
        /// <param name="sumInpayment">Sum of inpayments</param>
        /// <param name="sumCapitalEndofPeriod">Sum of the capital at the end of the period</param>
        /// <returns>Performance in %</returns>
        internal decimal CalculatePerformancePercentageGeometrical(decimal sumInpayment, decimal sumCapitalEndofPeriod)
        {
            if (sumCapitalEndofPeriod == 0)
                return default(decimal);

            return decimal.Round(Convert.ToDecimal((1 - (sumInpayment / sumCapitalEndofPeriod)) * 100), 2);
        }
    }
}
