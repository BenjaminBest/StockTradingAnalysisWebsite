using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StockTradingAnalysis.Domain.CQRS.Cmd.Commands;
using StockTradingAnalysis.Domain.CQRS.Cmd.Exceptions;
using StockTradingAnalysis.Domain.CQRS.Query.Queries;
using StockTradingAnalysis.Interfaces.Commands;
using StockTradingAnalysis.Interfaces.Domain;
using StockTradingAnalysis.Interfaces.Filter;
using StockTradingAnalysis.Interfaces.Queries;
using StockTradingAnalysis.Web.Common;
using StockTradingAnalysis.Web.Common.Interfaces;
using StockTradingAnalysis.Web.Common.ItemResolvers;
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
		public IActionResult Index(string timeFilter, string stockTypeFilter)
		{
			ViewBag.TimeFilter = _selectItemResolverRegistry.GetItems("TimePeriodRelative");
			ViewBag.StockTypeFilter = _selectItemResolverRegistry.GetItems("StockType");

			ViewBag.Performance = Mapper.Map<IEnumerable<TransactionPerformanceViewModel>>(
				_queryDispatcher.Execute(new TransactionPerformanceAllQuery())).ToDictionary(p => p.Id, p => p);

			//Filters
			var filters = new List<ITransactionFilter>();
			if (!string.IsNullOrEmpty(timeFilter))
				filters.AddRange(TimePeriodRelativeItemResolver.ResolveFilter(timeFilter));

			if (!string.IsNullOrEmpty(stockTypeFilter))
				filters.AddRange(StockTypeItemResolver.ResolveFilter(stockTypeFilter));

			return View(GetIndexTransactions(filters));
		}

		// GET: Transaction/Buy
		public IActionResult Buy()
		{
			ViewBag.Stocks = _selectItemResolverRegistry.GetItems("Stock");
			ViewBag.Strategies = _selectItemResolverRegistry.GetItems("Strategy");

			return View();
		}

		// POST: Transaction/Buy
		[HttpPost]
		public IActionResult Buy(TransactionBuyingViewModel model, FormCollection collection)
		{
			var id = Guid.NewGuid();

			try
			{
				IImage image = _imageService.GetImage(Request.Form.Files["Image"], collection["image.Description"], id);

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
		public IActionResult Sell()
		{
			ViewBag.Stocks = _selectItemResolverRegistry.GetItems("Stock");
			ViewBag.Feedbacks = _selectItemResolverRegistry.GetItems("Feedback");

			return View();
		}

		// POST: Transaction/Sell
		[HttpPost]
		public IActionResult Sell(TransactionSellingViewModel model, FormCollection collection)
		{
			var id = Guid.NewGuid();

			try
			{
				var image = _imageService.GetImage(Request.Form.Files["Image"], collection["image.Description"], id);

				var feedback = model.Feedback?.Select(f => f.Id) ?? new List<Guid>();

				_commandDispatcher.Execute(new TransactionSellCommand(id, model.OriginalVersion, model.OrderDate, model.Units, model.PricePerUnit, model.OrderCosts,
					model.Description, model.Tag, image, model.Stock.Id, model.Taxes, model.MAE, model.MFE, feedback));

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
		public IActionResult Dividend()
		{
			ViewBag.Stocks = _selectItemResolverRegistry.GetItems("Stock");
			ViewBag.Feedbacks = _selectItemResolverRegistry.GetItems("Feedback");

			return View();
		}

		// POST: Transaction/Dividend
		[HttpPost]
		public IActionResult Dividend(TransactionSellingViewModel model, FormCollection collection)
		{
			var id = Guid.NewGuid();

			try
			{
				IImage image = _imageService.GetImage(Request.Form.Files["Image"], collection["image.Description"], id);

				_commandDispatcher.Execute(new TransactionDividendCommand(id, model.OriginalVersion, model.OrderDate, model.Units, model.PricePerUnit, model.OrderCosts,
					model.Description, model.Tag, image, model.Stock.Id, model.Taxes));

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
		public IActionResult Split()
		{
			ViewBag.Stocks = _selectItemResolverRegistry.GetItems("Stock");

			return View();
		}

		// POST: Transaction/Split
		[HttpPost]
		public IActionResult Split(TransactionSplitViewModel model, FormCollection collection)
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
		public IActionResult Edit(Guid id)
		{
			ViewBag.Stocks = _selectItemResolverRegistry.GetItems("Stock");
			ViewBag.Strategies = _selectItemResolverRegistry.GetItems("Strategy");
			ViewBag.Feedbacks = _selectItemResolverRegistry.GetItems("Feedback");

			return View(GetTransaction(id));
		}

		// POST: Transaction/Edit/5
		[HttpPost]
		public IActionResult Edit(Guid id, TransactionViewModel model, FormCollection collection)
		{
			try
			{
				var image = _imageService.GetImage(Request.Form.Files["Image"], collection["image.Description"], id);

				if (string.IsNullOrEmpty(collection["deleteImage"]))
				{
					image = _imageService.GetImage(model.Image.Data, model.Image.OriginalName, model.Image.ContentType,
						model.Image.Description,
						id);
				}

				//TODO: Edit a transaction

				return RedirectToAction("Index");
			}
			catch (DomainValidationException validationException)
			{
				ModelState.AddModelError(validationException.Property, validationException.Message);
			}

			return View(model);
		}

		// GET: Transaction/Delete/5
		public IActionResult Delete(Guid id)
		{
			return View(GetTransaction(id));
		}

		// POST: Transaction/Delete/5
		[HttpPost]
		public IActionResult Delete(Guid id, TransactionViewModel model)
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
		public IActionResult GetTags(string term)
		{
			var items = _queryDispatcher.Execute(new TransactionTagSearchQuery(term));

			return Json(items.Take(10));
		}

		/// <summary>
		/// Get image by id
		/// </summary>
		/// <param name="id">Image ID</param>
		/// <returns>Image</returns>
		public IActionResult GetImage(Guid id)
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
		/// <param name="filters">The filters which should be applied to filter the transactions.</param>
		/// <returns>Transactions</returns>
		private IEnumerable<TransactionIndexViewModel> GetIndexTransactions(IEnumerable<ITransactionFilter> filters)
		{
			var balances = _queryDispatcher.Execute(new AccountBalanceAllQuery()).ToDictionary(a => a.TransactionId, a => a);

			var query = new TransactionAllQuery();

			foreach (var filter in filters)
			{
				query.Register(filter);
			}

			var models = Mapper.Map<IEnumerable<TransactionIndexViewModel>>(_queryDispatcher.Execute(query)).ToList();

			models.ForEach(m =>
			{
				m.AccountBalance = balances.TryGetValue(m.Id, out var balance) ? balance.Balance : decimal.Zero;
			});

			return models;
		}
	}
}