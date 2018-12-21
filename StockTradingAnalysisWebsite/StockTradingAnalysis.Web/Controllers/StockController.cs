using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StockTradingAnalysis.Domain.CQRS.Cmd.Commands;
using StockTradingAnalysis.Domain.CQRS.Cmd.Exceptions;
using StockTradingAnalysis.Domain.CQRS.Query.Queries;
using StockTradingAnalysis.Interfaces.Commands;
using StockTradingAnalysis.Interfaces.Queries;
using StockTradingAnalysis.Web.Common.Interfaces;
using StockTradingAnalysis.Web.Models;

namespace StockTradingAnalysis.Web.Controllers
{
	public class StockController : Controller
	{
		private readonly IQueryDispatcher _queryDispatcher;
		private readonly ICommandDispatcher _commandDispatcher;
		private readonly IQuotationServiceClient _quotationServiceClient;

		public StockController(
			IQueryDispatcher queryDispatcher,
			ICommandDispatcher commandDispatcher,
			IQuotationServiceClient quotationServiceClient)
		{
			_queryDispatcher = queryDispatcher;
			_commandDispatcher = commandDispatcher;
			_quotationServiceClient = quotationServiceClient;
		}

		// GET: Stock
		public IActionResult Index()
		{
			return View(Mapper.Map<IEnumerable<StockViewModel>>(_queryDispatcher.Execute(new StockAllQuery())));
		}

		// GET: Stock/Create
		public IActionResult Create()
		{
			return View();
		}

		// POST: Stock/Create
		[HttpPost]
		public IActionResult Create(StockViewModel model)
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
		public IActionResult Edit(Guid id)
		{
			return View(Mapper.Map<StockViewModel>(_queryDispatcher.Execute(new StockByIdQuery(id))));
		}

		// POST: Stock/Edit/5
		[HttpPost]
		public IActionResult Edit(Guid id, StockViewModel model)
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
		public IActionResult Delete(Guid id)
		{
			return View(Mapper.Map<StockViewModel>(_queryDispatcher.Execute(new StockByIdQuery(id))));
		}

		// POST: Stock/Delete/5
		[HttpPost]
		public IActionResult Delete(Guid id, StockViewModel model)
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

		// GET: Stock/UpdateQuotations
		public IActionResult UpdateQuotations()
		{
			var model = new UpdateQuotationsViewModel
			{
				IsServiceAvailable = _quotationServiceClient.IsOnline(),
				Stocks = Mapper.Map<IEnumerable<StockViewModel>>(_queryDispatcher.Execute(new StockAllQuery()))
			};

			return View(model);
		}

		// GET: Stock/UpdateQuotation/5
		public IActionResult UpdateQuotation(Guid id)
		{
			//TODO: Duplicate code, see QuotationController.UpdateQuotation
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
		public IActionResult GetStockName(string term)
		{
			var items = _queryDispatcher.Execute(new StockNameSearchQuery(term));

			return Json(items.Take(10));
		}

		/// <summary>
		/// Returns a list with all found stock wkns
		/// </summary>
		/// <param name="term"></param>
		/// <returns></returns>
		public IActionResult GetStockWkn(string term)
		{
			var items = _queryDispatcher.Execute(new StockWknSearchQuery(term));

			return Json(items.Take(10));
		}

		/// <summary>
		/// Returns a list with all found stock types
		/// </summary>
		/// <param name="term"></param>
		/// <returns></returns>
		public IActionResult GetStockType(string term)
		{
			var items = _queryDispatcher.Execute(new StockTypeSearchQuery(term));

			return Json(items.Take(10));
		}
	}
}