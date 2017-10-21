using AutoMapper;
using StockTradingAnalysis.Core.Common;
using StockTradingAnalysis.Domain.CQRS.Cmd.Commands;
using StockTradingAnalysis.Domain.CQRS.Cmd.Exceptions;
using StockTradingAnalysis.Domain.CQRS.Query.Queries;
using StockTradingAnalysis.Domain.Events.Domain;
using StockTradingAnalysis.Interfaces.Commands;
using StockTradingAnalysis.Interfaces.Configuration;
using StockTradingAnalysis.Interfaces.Queries;
using StockTradingAnalysis.Interfaces.Services;
using StockTradingAnalysis.Web.Common.Configuration;
using StockTradingAnalysis.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace StockTradingAnalysis.Web.Controllers
{
    public class StockController : Controller
    {
        private readonly IQueryDispatcher _queryDispatcher;
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly ISerializerService _serializerService;
        private readonly IConfigurationRegistry _configurationRegistry;

        public StockController(
            IQueryDispatcher queryDispatcher,
            ICommandDispatcher commandDispatcher,
            ISerializerService serializerService,
            IConfigurationRegistry configurationRegistry)
        {
            _queryDispatcher = queryDispatcher;
            _commandDispatcher = commandDispatcher;
            _serializerService = serializerService;
            _configurationRegistry = configurationRegistry;
        }

        // GET: Stock
        public ActionResult Index()
        {
            return View(Mapper.Map<IEnumerable<StockViewModel>>(_queryDispatcher.Execute(new StockAllQuery())));
        }

        // GET: Stock/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Stock/Create
        [HttpPost]
        public ActionResult Create(StockViewModel model)
        {
            var id = Guid.NewGuid();

            try
            {
                _commandDispatcher.Execute(new StockAddCommand(id, model.OriginalVersion, model.Name, model.Wkn,
                    model.Type, model.LongShort));

                return RedirectToAction("Index");
            }
            catch (DomainValidationException validationException)
            {
                ModelState.AddModelError(validationException.Property, validationException.Message);
            }

            return View(model);
        }

        // GET: Stock/Edit/5
        public ActionResult Edit(Guid id)
        {
            return View(Mapper.Map<StockViewModel>(_queryDispatcher.Execute(new StockByIdQuery(id))));
        }

        // POST: Stock/Edit/5
        [HttpPost]
        public ActionResult Edit(Guid id, StockViewModel model)
        {
            try
            {
                _commandDispatcher.Execute(new StockChangeCommand(id, model.OriginalVersion, model.Name, model.Wkn,
                    model.Type, model.LongShort));

                return RedirectToAction("Index");
            }
            catch (DomainValidationException validationException)
            {
                ModelState.AddModelError(validationException.Property, validationException.Message);
            }

            return View(model);
        }

        // GET: Stock/Delete/5
        public ActionResult Delete(Guid id)
        {
            return View(Mapper.Map<StockViewModel>(_queryDispatcher.Execute(new StockByIdQuery(id))));
        }

        // POST: Stock/Delete/5
        [HttpPost]
        public ActionResult Delete(Guid id, StockViewModel model)
        {
            try
            {
                _commandDispatcher.Execute(new StockRemoveCommand(id, model.OriginalVersion));

                return RedirectToAction("Index");
            }
            catch (DomainValidationException validationException)
            {
                ModelState.AddModelError(validationException.Property, validationException.Message);
            }

            return View(model);
        }

        public ActionResult UpdateQuotations()
        {
            return View(Mapper.Map<IEnumerable<StockViewModel>>(_queryDispatcher.Execute(new StockAllQuery())));
        }

        // GET: Stock/UpdateQuotation/5
        public ActionResult UpdateQuotation(Guid id)
        {
            var stock = _queryDispatcher.Execute(new StockByIdQuery(id));

            if (stock == null)
            {
                return PartialView("DisplayTemplates/UpdateStatus", new UpdateQuotationStatusViewModel()
                {
                    Message = Resources.NoSuchStock,
                    Successfull = false
                });
            }

            //Quotations before
            var quotationsBefore = _queryDispatcher.Execute(new StockQuotationsCountByIdQuery(id));

            var result = HtmlDownload.CreateHttpClientSync(new Uri(_configurationRegistry.GetValue<string>(ConfigurationKeys.StockQuoteServiceBaseUrl) + $"/{stock.Wkn}"));

            if (string.IsNullOrEmpty(result))
            {
                return PartialView("DisplayTemplates/UpdateStatus", new UpdateQuotationStatusViewModel()
                {
                    Message = Resources.NoSuchStock,
                    Successfull = false
                });
            }

            var quotations = _serializerService.Deserialize<IEnumerable<Quotation>>(result).ToList();

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

            return PartialView("DisplayTemplates/UpdateStatus", new UpdateQuotationStatusViewModel()
            {
                Id = stock.Id,
                Message = string.Format(Resources.StatusQuotations, existentQuotations, existentQuotations - quotationsBefore),
                Successfull = quotations.Any()
            });
        }


        /// <summary>
        /// Returns a list with all found stock names
        /// </summary>
        /// <param name="term"></param>
        /// <returns></returns>
        public JsonResult GetStockName(string term)
        {
            var items = _queryDispatcher.Execute(new StockNameSearchQuery(term));

            return Json(items.Take(10), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Returns a list with all found stock wkns
        /// </summary>
        /// <param name="term"></param>
        /// <returns></returns>
        public JsonResult GetStockWkn(string term)
        {
            var items = _queryDispatcher.Execute(new StockWknSearchQuery(term));

            return Json(items.Take(10), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Returns a list with all found stock types
        /// </summary>
        /// <param name="term"></param>
        /// <returns></returns>
        public JsonResult GetStockType(string term)
        {
            var items = _queryDispatcher.Execute(new StockTypeSearchQuery(term));

            return Json(items.Take(10), JsonRequestBehavior.AllowGet);
        }
    }
}