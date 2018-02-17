using System;
using System.Linq;
using System.Web.Mvc;
using StockTradingAnalysis.Domain.CQRS.Query.Queries;
using StockTradingAnalysis.Interfaces.Domain;
using StockTradingAnalysis.Interfaces.Queries;
using StockTradingAnalysis.Interfaces.Services.Core;

namespace StockTradingAnalysis.Web.Controllers
{
    /// <summary>
    /// The chart controller is used to render charts
    /// </summary>
    /// <seealso cref="Controller" />
    public class ChartController : Controller
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
        /// Initializes a new instance of the <see cref="ChartController" /> class.
        /// </summary>
        /// <param name="queryDispatcher">The query dispatcher.</param>
        /// <param name="dateCalculationService">The date calculation service.</param>
        public ChartController(IQueryDispatcher queryDispatcher, IDateCalculationService dateCalculationService)
        {
            _queryDispatcher = queryDispatcher;
            _dateCalculationService = dateCalculationService;
        }

        // GET: Chart/CandlestickChartQuoteDataByStockId/1
        [HttpGet]
        public ActionResult CandlestickChartQuoteDataByStockId(Guid id)
        {
            return Json(_queryDispatcher.Execute(new StockQuotationsByIdQuery(id)).Select(
                    q => new object[]
                    {
                        _dateCalculationService.ConvertToEpochTimeInMilliseconds(q.Date), q.Open, q.High, q.Low, q.Close
                    })
                .ToArray(), JsonRequestBehavior.AllowGet);
        }

        // GET: Chart/CandlestickChartTransactionDataByStockId/1
        [HttpGet]
        public ActionResult CandlestickChartTransactionDataByStockId(Guid id)
        {
            return Json(_queryDispatcher.Execute(new TransactionsByStockIdQuery(id)).Select(
                t => new
                {
                    x = _dateCalculationService.ConvertToEpochTimeInMilliseconds(t.OrderDate),
                    title = GetToolTipFromTransaction(t).Title,
                    text = GetToolTipFromTransaction(t).Text,
                    fillColor = GetToolTipFromTransaction(t).Color,
                    style = new { color = "white" }
                }).ToArray(), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Gets tooltip information for the given transaction.
        /// </summary>
        /// <param name="transaction">The transaction.</param>
        /// <returns></returns>
        private static dynamic GetToolTipFromTransaction(ITransaction transaction)
        {
            if (transaction is IBuyingTransaction)
            {
                return new
                {
                    Text = string.Format(Resources.ViewTextCandleStickChartToolTipBuy, transaction.Shares, transaction.PricePerShare, transaction.OrderDate),
                    Title = Resources.ViewTextCandleStickChartToolTipBuyShortCut,
                    Color = "#62c462"
                };
            }
            if (transaction is ISellingTransaction)
            {
                return new
                {
                    Text = string.Format(Resources.ViewTextCandleStickChartToolTipSell, transaction.Shares, transaction.PricePerShare, transaction.OrderDate),
                    Title = Resources.ViewTextCandleStickChartToolTipSellShortCut,
                    Color = "#ee5f5b"
                };
            }
            if (transaction is IDividendTransaction)
            {
                return new
                {
                    Text = string.Format(Resources.ViewTextCandleStickChartToolTipDividend, transaction.Shares, transaction.PricePerShare, transaction.OrderDate),
                    Title = Resources.ViewTextCandleStickChartToolTipDividendShortCut,
                    Color = "#007bff"
                };
            }
            if (transaction is ISplitTransaction)
            {
                return new
                {
                    Text = string.Format(Resources.ViewTextCandleStickChartToolTipSplit, transaction.Shares, transaction.PricePerShare, transaction.OrderDate),
                    Title = Resources.ViewTextCandleStickChartToolTipSplitShortCut,
                    Color = "#89406"
                };
            }

            return new
            {
                Color = "#7A8288",
                Title = "-",
                Text = string.Empty
            };
        }
    }
}