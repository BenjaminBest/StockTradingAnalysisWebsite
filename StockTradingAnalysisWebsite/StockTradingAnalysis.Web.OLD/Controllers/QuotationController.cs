using System;
using System.Linq;
using System.Web.Mvc;
using StockTradingAnalysis.Domain.CQRS.Cmd.Commands;
using StockTradingAnalysis.Domain.CQRS.Query.Queries;
using StockTradingAnalysis.Interfaces.Commands;
using StockTradingAnalysis.Interfaces.Queries;
using StockTradingAnalysis.Web.Common.Interfaces;
using StockTradingAnalysis.Web.Models;

namespace StockTradingAnalysis.Web.Controllers
{
    public class QuotationController : Controller
    {
        private readonly IQuotationServiceClient _quotationServiceClient;
        private readonly IQueryDispatcher _queryDispatcher;
        private readonly ICommandDispatcher _commandDispatcher;

        public QuotationController(
            IQuotationServiceClient quotationServiceClient,
            IQueryDispatcher queryDispatcher,
            ICommandDispatcher commandDispatcher)
        {
            _quotationServiceClient = quotationServiceClient;
            _queryDispatcher = queryDispatcher;
            _commandDispatcher = commandDispatcher;
        }

        // GET: Quotation/IsQuotationServiceOnline
        [HttpGet]
        public JsonResult IsQuotationServiceOnline()
        {
            return Json(_quotationServiceClient.IsOnline(), JsonRequestBehavior.AllowGet);
        }

        // GET: Quotation/UpdateQuotationByWkn/BASF11
        [HttpPost]
        public JsonResult UpdateQuotationByWkn(string wkn)
        {
            var stock = _queryDispatcher.Execute(new StockByWknQuery(wkn));

            if (stock == null)
            {
                return Json(new UpdateQuotationStatusViewModel()
                {
                    Message = Resources.NoSuchStock,
                    Successfull = false
                });
            }

            return UpdateQuotation(stock.Id);
        }

        // GET: Quotation/UpdateQuotation/5
        [HttpPost]
        public JsonResult UpdateQuotation(Guid id)
        {
            var stock = _queryDispatcher.Execute(new StockByIdQuery(id));

            if (stock == null)
            {
                return Json(new UpdateQuotationStatusViewModel()
                {
                    Message = Resources.NoSuchStock,
                    Successfull = false
                });
            }

            //Quotations before
            var quotationsBefore = _queryDispatcher.Execute(new StockQuotationsCountByIdQuery(id));

            var quotations = _quotationServiceClient.Get(stock.Id).ToList();

            if (quotations.Any())
            {
                var cmd = new StockQuotationsAddOrChangeCommand(
                    stock.Id,
                    stock.OriginalVersion,
                    quotations);

                _commandDispatcher.Execute(cmd);
            }

            //Statistics
            var existentQuotations = _queryDispatcher.Execute(new StockQuotationsCountByIdQuery(id));

            return Json(new UpdateQuotationStatusViewModel()
            {
                Id = stock.Id,
                Message = string.Format(Resources.StatusQuotations, existentQuotations, existentQuotations - quotationsBefore),
                Successfull = quotations.Any()
            });
        }
    }
}