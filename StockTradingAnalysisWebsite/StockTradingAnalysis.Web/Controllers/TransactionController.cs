using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using StockTradingAnalysis.Domain.CQRS.Cmd.Commands;
using StockTradingAnalysis.Domain.CQRS.Cmd.Exceptions;
using StockTradingAnalysis.Domain.CQRS.Query.Queries;
using StockTradingAnalysis.Interfaces.Commands;
using StockTradingAnalysis.Interfaces.Domain;
using StockTradingAnalysis.Interfaces.Queries;
using StockTradingAnalysis.Interfaces.Services;
using StockTradingAnalysis.Web.Common;
using StockTradingAnalysis.Web.Common.Interfaces;
using StockTradingAnalysis.Web.Models;

namespace StockTradingAnalysis.Web.Controllers
{
    public class TransactionController : Controller
    {
        private readonly IQueryDispatcher _queryDispatcher;
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IImageService _imageService;
        private readonly ISelectItemResolverRegistry _selectItemResolverRegistry;

        public TransactionController(
            IQueryDispatcher queryDispatcher,
            ICommandDispatcher commandDispatcher,
            IImageService imageService,
            ISelectItemResolverRegistry selectItemResolverRegistry)
        {
            _queryDispatcher = queryDispatcher;
            _commandDispatcher = commandDispatcher;
            _imageService = imageService;
            _selectItemResolverRegistry = selectItemResolverRegistry;
        }

        // GET: Transaction
        public ActionResult Index()
        {
            ViewBag.TimeFilter = _selectItemResolverRegistry.GetItems("TimePeriodRelative");
            ViewBag.StockTypeFilter = _selectItemResolverRegistry.GetItems("StockType");

            ViewBag.Performance = Mapper.Map<IEnumerable<TransactionPerformanceViewModel>>(
                _queryDispatcher.Execute(new TransactionPerformanceAllQuery())).ToDictionary(p => p.Id, p => p);

            //TODO: Apply filter here

            return View(GetIndexTransactions());
        }

        // GET: Transaction/Buy
        public ActionResult Buy()
        {
            ViewBag.Stocks = _selectItemResolverRegistry.GetItems("Stock");
            ViewBag.Strategies = _selectItemResolverRegistry.GetItems("Strategy");

            return View();
        }

        // POST: Transaction/Buy
        [HttpPost]
        public ActionResult Buy(TransactionBuyingViewModel model, FormCollection collection)
        {
            var id = Guid.NewGuid();

            try
            {
                IImage image = null;

                if (Request.Files["Image"] != null && Request.Files["Image"].ContentLength != 0)
                {
                    image = _imageService.GetImage(Request.Files["Image"], collection["image.Description"], id);
                }

                _commandDispatcher.Execute(new TransactionBuyCommand(id, model.OriginalVersion, model.OrderDate, model.Units,
                    model.PricePerUnit, model.OrderCosts, model.Description, model.Tag, image, model.InitialSL, model.InitialTP, model.Stock.Id, model.Strategy.Id));


                return RedirectToAction("Index");
            }
            catch (DomainValidationException validationException)
            {
                ModelState.AddModelError(validationException.Property, validationException.Message);
            }

            ViewBag.Stocks = _selectItemResolverRegistry.GetItems("Stock");
            ViewBag.Strategies = _selectItemResolverRegistry.GetItems("Strategy");

            return View(model);
        }

        // GET: Transaction/Sell
        public ActionResult Sell()
        {
            ViewBag.Stocks = _selectItemResolverRegistry.GetItems("Stock");
            ViewBag.Feedbacks = _selectItemResolverRegistry.GetItems("Feedback");

            return View();
        }

        // POST: Transaction/Sell
        [HttpPost]
        public ActionResult Sell(TransactionSellingViewModel model, FormCollection collection)
        {
            var id = Guid.NewGuid();

            try
            {
                IImage image = null;

                if (Request.Files["Image"] != null && Request.Files["Image"].ContentLength != 0)
                {
                    image = _imageService.GetImage(Request.Files["Image"], collection["image.Description"], id);
                }

                var feedback = model.Feedback?.Select(f => f.Id) ?? new List<Guid>();

                _commandDispatcher.Execute(new TransactionSellCommand(id, model.OriginalVersion, model.OrderDate, model.Units, model.PricePerUnit, model.OrderCosts,
                    model.Description, model.Tag, image, model.Stock.Id, model.Taxes, model.MAE, model.MFE, feedback));

                _commandDispatcher.Execute(new TransactionCalculateCommand(id, _queryDispatcher.Execute(new TransactionByIdQuery(id)).OriginalVersion));

                return RedirectToAction("Index");
            }
            catch (DomainValidationException validationException)
            {
                ModelState.AddModelError(validationException.Property, validationException.Message);
            }


            ViewBag.Stocks = _selectItemResolverRegistry.GetItems("Stock");
            ViewBag.Feedbacks = _selectItemResolverRegistry.GetItems("Feedback");

            return View(model);
        }

        // GET: Transaction/Dividend
        public ActionResult Dividend()
        {
            ViewBag.Stocks = _selectItemResolverRegistry.GetItems("Stock");
            ViewBag.Feedbacks = _selectItemResolverRegistry.GetItems("Feedback");

            return View();
        }

        // POST: Transaction/Dividend
        [HttpPost]
        public ActionResult Dividend(TransactionSellingViewModel model, FormCollection collection)
        {
            var id = Guid.NewGuid();

            try
            {
                IImage image = null;

                if (Request.Files["Image"] != null && Request.Files["Image"].ContentLength != 0)
                {
                    image = _imageService.GetImage(Request.Files["Image"], collection["image.Description"], id);
                }

                _commandDispatcher.Execute(new TransactionDividendCommand(id, model.OriginalVersion, model.OrderDate, model.Units, model.PricePerUnit, model.OrderCosts,
                    model.Description, model.Tag, image, model.Stock.Id, model.Taxes));

                _commandDispatcher.Execute(new TransactionCalculateDividendCommand(id, _queryDispatcher.Execute(new TransactionByIdQuery(id)).OriginalVersion));

                return RedirectToAction("Index");
            }
            catch (DomainValidationException validationException)
            {
                ModelState.AddModelError(validationException.Property, validationException.Message);
            }

            ViewBag.Stocks = _selectItemResolverRegistry.GetItems("Stock");
            ViewBag.Feedbacks = _selectItemResolverRegistry.GetItems("Feedback");

            return View(model);
        }

        // GET: Transaction/Split
        public ActionResult Split()
        {
            ViewBag.Stocks = _selectItemResolverRegistry.GetItems("Stock");

            return View();
        }

        // POST: Transaction/Split
        [HttpPost]
        public ActionResult Split(TransactionSplitViewModel model, FormCollection collection)
        {
            var id = Guid.NewGuid();

            try
            {
                _commandDispatcher.Execute(new TransactionSplitCommand(id, model.OriginalVersion, model.OrderDate, model.Units, model.PricePerUnit, model.Stock.Id));

                return RedirectToAction("Index");
            }
            catch (DomainValidationException validationException)
            {
                ModelState.AddModelError(validationException.Property, validationException.Message);
            }

            ViewBag.Stocks = _selectItemResolverRegistry.GetItems("Stock");

            return View(model);
        }

        // GET: Transaction/Edit/5
        public ActionResult Edit(Guid id)
        {
            ViewBag.Stocks = _selectItemResolverRegistry.GetItems("Stock");
            ViewBag.Strategies = _selectItemResolverRegistry.GetItems("Strategy");
            ViewBag.Feedbacks = _selectItemResolverRegistry.GetItems("Feedback");

            return View(GetTransaction(id));
        }

        // POST: Transaction/Edit/5
        [HttpPost]
        public ActionResult Edit(Guid id, TransactionViewModel model, FormCollection collection)
        {
            try
            {
                IImage image = null;

                if (Request.Files["Image"] != null && Request.Files["Image"].ContentLength != 0)
                {
                    image = _imageService.GetImage(Request.Files["Image"], collection["image.Description"], id);
                }
                else if (string.IsNullOrEmpty(collection["deleteImage"]))
                {
                    image = _imageService.GetImage(model.Image.Data, model.Image.OriginalName, model.Image.ContentType,
                        model.Image.Description,
                        id);
                }

                //_commandDispatcher.Execute(new StockChangeCommand(id, model.OriginalVersion, model.Name, model.Wkn,
                //    model.Type, model.IsDividend,
                //    model.LongShort));

                return RedirectToAction("Index");
            }
            catch (DomainValidationException validationException)
            {
                ModelState.AddModelError(validationException.Property, validationException.Message);
            }

            return View(model);
        }

        // GET: Transaction/Delete/5
        public ActionResult Delete(Guid id)
        {
            return View(GetTransaction(id));
        }

        // POST: Transaction/Delete/5
        [HttpPost]
        public ActionResult Delete(Guid id, TransactionViewModel model)
        {
            try
            {
                _commandDispatcher.Execute(new TransactionRemoveCommand(id, model.OriginalVersion));

                return RedirectToAction("Index");
            }
            catch (DomainValidationException validationException)
            {
                ModelState.AddModelError(validationException.Property, validationException.Message);
            }

            return View(model);
        }

        /// <summary>
        /// Returns a list with all found tags
        /// </summary>
        /// <param name="term">Search text</param>
        /// <returns></returns>
        public JsonResult GetTags(string term)
        {
            var items = _queryDispatcher.Execute(new TransactionTagSearchQuery(term));

            return Json(items.Take(10), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Get image by id
        /// </summary>
        /// <param name="id">Image ID</param>
        /// <returns>Image</returns>
        public ActionResult GetImage(Guid id)
        {
            var image = _queryDispatcher.Execute(new TransactionImageByIdQuery(id));

            return image == null ? null : new ImageResult(image.Data, image.ContentType);
        }

        /// <summary>
        /// Returns the transaction as <seealso cref="TransactionSellingViewModel"/>, <seealso cref="TransactionBuyingViewModel"/>
        /// or <seealso cref="TransactionDividendViewModel"/>
        /// </summary>
        /// <param name="id">The id of the transaction</param>
        /// <returns>Transaction</returns>
        private TransactionViewModel GetTransaction(Guid id)
        {
            var transaction = _queryDispatcher.Execute(new TransactionByIdQuery(id));

            if (transaction is IBuyingTransaction)
                return Mapper.Map<TransactionBuyingViewModel>(transaction);

            if (transaction is IDividendTransaction)
                return Mapper.Map<TransactionDividendViewModel>(transaction);

            if (transaction is ISplitTransaction)
                return Mapper.Map<TransactionSplitViewModel>(transaction);

            return Mapper.Map<TransactionSellingViewModel>(transaction);
        }

        /// <summary>
        /// Returns all transactions
        /// </summary>
        /// <returns>Transaction</returns>
        private IEnumerable<TransactionIndexViewModel> GetIndexTransactions()
        {
            var balances = _queryDispatcher.Execute(new AccountBalanceAllQuery()).ToDictionary(a => a.TransactionId, a => a);

            var models = Mapper.Map<IEnumerable<TransactionIndexViewModel>>(_queryDispatcher.Execute(new TransactionAllQuery())).ToList();

            models.ForEach(m =>
            {
                IAccountBalance balance;
                m.AccountBalance = balances.TryGetValue(m.Id, out balance) ? balance.Balance : decimal.Zero;
            });

            return models;
        }
    }
}