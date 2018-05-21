using System;
using System.Collections.Generic;
using System.Linq;
using StockTradingAnalysis.Core.Extensions;
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
    /// The class TransactionCalculationService implements calulation methods based on transactions purely.
    /// </summary>
    /// <seealso cref="ITransactionCalculationService" />
    public class TransactionCalculationService : ITransactionCalculationService
    {
        /// <summary>
        /// The query dispatcher
        /// </summary>
        private readonly IQueryDispatcher _queryDispatcher;

        /// <summary>
        /// The date calculation service
        /// </summary>
        private readonly IDateCalculationService _dateCalculationService;

        /// <summary>
        /// The iir calculator service
        /// </summary>
        private readonly IInterestRateCalculatorService _iirCalculatorService;

        /// <summary>
        /// The transaction performance service
        /// </summary>
        private readonly ITransactionPerformanceService _transactionPerformanceService;

        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionCalculationService" /> class.
        /// </summary>
        /// <param name="queryDispatcher">The query dispatcher.</param>
        /// <param name="dateCalculationService">The date calculation service.</param>
        /// <param name="iirCalculatorService">The iir calculator service.</param>
        /// <param name="transactionPerformanceService">The transaction performance service.</param>
        public TransactionCalculationService(
            IQueryDispatcher queryDispatcher,
            IDateCalculationService dateCalculationService,
            IInterestRateCalculatorService iirCalculatorService,
            ITransactionPerformanceService transactionPerformanceService)
        {
            _queryDispatcher = queryDispatcher;
            _dateCalculationService = dateCalculationService;
            _iirCalculatorService = iirCalculatorService;
            _transactionPerformanceService = transactionPerformanceService;
        }

        /// <summary>
        /// Calculates the sum of all inpayments for the given transactions.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns>Sum of inpayments</returns>
        public decimal CalculateSumInpayments(IQuery<IEnumerable<ITransaction>> query)
        {
            var transactions = _queryDispatcher.Execute(query);

            var sum = default(decimal);

            foreach (var tr in transactions)
            {
                if (tr is IBuyingTransaction)
                {
                    sum += tr.PositionSize - tr.OrderCosts;
                }
            }

            return sum;
        }

        /// <summary>
        /// Calculates the sum of capital for the given transactions at the time <paramref name="date"/>
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="date">The date.</param>
        /// <returns></returns>
        public decimal CalculateSumCapital(IQuery<IEnumerable<ITransaction>> query, DateTime date)
        {
            var transactions = _queryDispatcher.Execute(query).ToList();

            var sumCapital = default(decimal);

            var stocks = transactions.Where(t => !(t is IDividendTransaction)).GroupBy(t => t.Stock).Select(t => t.Key);

            foreach (var stock in stocks)
            {
                var filtered = transactions.Where(t => t.Stock.Id == stock.Id).OrderByDescending(t => t.OrderDate);

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

                var quotation = _queryDispatcher.Execute(new StockQuotationsLastBeforeDateByIdQuery(stock.Id, date));

                if (quotation == null)
                {
                    sumCapital += (filtered.FirstOrDefault().PricePerShare * allUnitsOfStock);
                }
                else
                {
                    sumCapital += (quotation.Close * allUnitsOfStock);
                }
            }

            return sumCapital;
        }

        /// <summary>
        /// Calculates the sum of all dividends for the given transactions (without taxes and order costs).
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns>Sum of inpayments</returns>
        public decimal CalculateSumDividends(IQuery<IEnumerable<ITransaction>> query)
        {
            var transactions = _queryDispatcher.Execute(query).ToList();

            if (!transactions.Any())
                return default(decimal);

            return transactions.Where(t => t is IDividendTransaction).Cast<IDividendTransaction>().Sum(tr => tr.PositionSize - tr.OrderCosts - tr.Taxes);
        }

        /// <summary>
        /// The performance of the current period is calculated as if all shares whould have been sold at the beginning
        /// of this period and then the difference to the value at the end of the period is beeing calculated.
        /// All buys within the timeframe will be calculated seperatly on a daily basis.
        /// This calculation should only be used for periods starting with the begin of a year till the end of a year (IIR problem)
        /// </summary>
        /// <param name="query">The query should only contain the base query with special filters. Date range filters will automatically applied.</param>
        /// <param name="start">The start.</param>
        /// <param name="end">The end.</param>
        /// <returns></returns>
        public decimal CalculatePerformancePercentageForPeriod(IQuery<IEnumerable<ITransaction>> query, DateTime start, DateTime end)
        {
            var endOfLastYear = _dateCalculationService.GetEndDateOfYear(new DateTime(start.Year - 1, 1, 1));

            if (!(query is TransactionAllQuery localQuery))
                throw new InvalidOperationException("CalculatePerformancePercentageForPeriod only works with a TransactionAllQuery"); //TODO: Not good

            var periodQuery = new TransactionAllQuery().CopyFiltersFrom(localQuery).Register(new TransactionStartDateFilter(start)).Register(new TransactionEndDateFilter(end));
            var endOfYearQuery = new TransactionAllQuery().CopyFiltersFrom(localQuery).Register(new TransactionEndDateFilter(end));
            var lastYearQuery = new TransactionAllQuery().CopyFiltersFrom(localQuery).Register(new TransactionEndDateFilter(endOfLastYear));

            var cashFlowDepotValue = new CashFlow(CalculateSumCapital(endOfYearQuery, end), end);
            var cashFlowEndLastYear = new CashFlow(CalculateSumCapital(lastYearQuery, endOfLastYear) * -1, endOfLastYear);

            return CalculatePerformancePercentageIir(periodQuery, cashFlowEndLastYear, cashFlowDepotValue);
        }

        /// <summary>
        /// Calculates the performance with the internal rate of interest
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="beginPeriod">The begin period.</param>
        /// <param name="endPeriod">The end period.</param>
        /// <returns>  Performance in %  </returns>
        public decimal CalculatePerformancePercentageIir(IQuery<IEnumerable<ITransaction>> query, CashFlow beginPeriod, CashFlow endPeriod)
        {
            var transactions = _queryDispatcher.Execute(query);

            var cashflows = new List<CashFlow>();

            if (beginPeriod != null)
                cashflows.Add(beginPeriod);

            foreach (var tr in transactions.OrderBy(t => t.OrderDate))
            {
                if (tr is IBuyingTransaction)
                {
                    cashflows.Add(new CashFlow((tr.PositionSize + tr.OrderCosts) * -1, tr.OrderDate));
                }
                else if (tr is ISellingTransaction)
                {
                    cashflows.Add(new CashFlow(tr.PositionSize - tr.OrderCosts - ((ISellingTransaction)tr).Taxes, tr.OrderDate));
                }
                else if (tr is IDividendTransaction)
                {
                    cashflows.Add(new CashFlow(tr.PositionSize - tr.OrderCosts - ((IDividendTransaction)tr).Taxes, tr.OrderDate));
                }
            }

            cashflows.Add(endPeriod);

            var val = _iirCalculatorService.Calculate(cashflows);

            return decimal.Round(Convert.ToDecimal(val) * 100, 2);
        }

        /// <summary>
        /// Calculates the average buying prices.
        /// </summary>
        /// <param name="transactions">The transactions.</param>
        /// <returns></returns>
        public IEnumerable<IAverageBuyingPrice> CalculateAverageBuyingPrices(IOrderedEnumerable<ITransaction> transactions)
        {
            var filteredTransactions = transactions.OfTypes<ITransaction, IBuyingTransaction, ISellingTransaction>().ToList();

            if (!filteredTransactions.Any())
                return Enumerable.Empty<IAverageBuyingPrice>();

            var result = new List<IAverageBuyingPrice>();

            var accumulatedShares = 0m;
            var accumulatedPositionSize = 0m;

            foreach (var transaction in filteredTransactions)
            {
                if (transaction is IBuyingTransaction)
                {

                    accumulatedPositionSize += transaction.PositionSize;
                    accumulatedShares += transaction.Shares;
                }

                if (accumulatedShares != 0)
                    result.Add(new AverageBuyingPrice(transaction.OrderDate, decimal.Round(accumulatedPositionSize / accumulatedShares, 2)));

                if (transaction is ISellingTransaction)
                {
                    accumulatedPositionSize -= transaction.PositionSize;
                    accumulatedShares -= transaction.Shares;
                }
            }

            return result;
        }

        /// <summary>
        /// Calculates open positions.
        /// </summary>
        /// <returns>Detailed information about open positions.</returns>
        public IDetailedOpenPositionOverview CalculateOpenPositions()
        {
            var calculateProfit = new Func<IOpenPosition, IQuotation, IProfit>((pos, quote) =>
                _transactionPerformanceService.GetProfit(
                    pos.Shares * pos.PricePerShare,
                    pos.OrderCosts,
                    pos.Shares * (quote?.Close ?? 0),
                    0m,
                    0m));

            var result = new DetailedOpenPositionOverview
            {
                OpenPositions = _queryDispatcher.Execute(new OpenPositionsAllQuery())
                    .Select(pos =>
                    {
                        var quote = _queryDispatcher.Execute(new StockQuotationsLatestByIdQuery(pos.ProductId));

                        return new DetailedOpenPosition(_queryDispatcher.Execute(new StockByIdQuery(pos.ProductId)))
                        {
                            AveragePricePerShare = pos.PricePerShare,
                            FirstOrderDate = pos.FirstOrderDate,
                            PositionSize = pos.PositionSize,
                            Shares = pos.Shares,
                            CurrentQuotation = quote,
                            Profit = calculateProfit(pos, quote),
                            OrderCosts = pos.OrderCosts
                        };
                    }).ToList()
            };

            result.Summary = new DetailedOpenPositionSummary
            {
                PositionSize = result.OpenPositions.Sum(o => o.PositionSize),
                Profit = _transactionPerformanceService.GetProfit(
                    result.OpenPositions.Sum(o => o.PositionSize),
                    0m,
                    result.OpenPositions.Sum(o => (o.CurrentQuotation?.Close ?? 0) * o.Shares),
                    0m,
                    0m)
            };

            return result;
        }
    }
}
