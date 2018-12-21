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
using StockTradingAnalysis.Interfaces.Queries;
using StockTradingAnalysis.Web.Common;
using StockTradingAnalysis.Web.Common.Interfaces;
using StockTradingAnalysis.Web.Models;

namespace StockTradingAnalysis.Web.Controllers
{
	public class StrategyController : Controller
	{
		private readonly IQueryDispatcher _queryDispatcher;
		private readonly ICommandDispatcher _commandDispatcher;
		private readonly IImageService _imageService;

		public StrategyController(
			IQueryDispatcher queryDispatcher,
			ICommandDispatcher commandDispatcher,
			IImageService imageService)
		{
			_queryDispatcher = queryDispatcher;
			_commandDispatcher = commandDispatcher;
			_imageService = imageService;
		}

		// GET: Strategy
		public IActionResult Index()
		{
			return View(Mapper.Map<IEnumerable<StrategyViewModel>>(_queryDispatcher.Execute(new StrategyAllQuery())));
		}

		// GET: Strategy/Create
		public IActionResult Create()
		{
			return View();
		}

		// POST: Strategy/Create
		[HttpPost]
		public IActionResult Create(StrategyViewModel model, FormCollection collection)
		{
			var id = Guid.NewGuid();

			try
			{
				IImage image = _imageService.GetImage(Request.Form.Files.GetFile("Image"), collection["image.Description"], id);

				_commandDispatcher.Execute(new StrategyAddCommand(id, model.OriginalVersion, model.Name,
					model.Description, image));

				return RedirectToAction("Index");
			}
			catch (DomainValidationException validationException)
			{
				ModelState.AddModelError(validationException.Property, validationException.Message);
			}

			return View(model);
		}

		// GET: Strategy/Edit/5
		public IActionResult Edit(Guid id)
		{
			return View(Mapper.Map<StrategyViewModel>(_queryDispatcher.Execute(new StrategyByIdQuery(id))));
		}

		// POST: Strategy/Edit/5
		[HttpPost]
		public IActionResult Edit(Guid id, StrategyViewModel model, FormCollection collection)
		{
			try
			{
				IImage image = null;

				if (Request.Form.Files["Image"] != null && Request.Form.Files["Image"].Length != 0)
				{
					image = _imageService.GetImage(Request.Form.Files.GetFile("Image"), collection["image.Description"], id);
				}
				else if (string.IsNullOrEmpty(collection["deleteImage"]))
				{
					image = _imageService.GetImage(model.Image.Data, model.Image.OriginalName, model.Image.ContentType,
						model.Image.Description,
						id);
				}

				_commandDispatcher.Execute(new StrategyChangeCommand(id, model.OriginalVersion, model.Name,
					model.Description, image));

				return RedirectToAction("Index");
			}
			catch (DomainValidationException validationException)
			{
				ModelState.AddModelError(validationException.Property, validationException.Message);
			}

			return View(model);
		}

		// GET: Strategy/Delete/5
		public IActionResult Delete(Guid id)
		{
			return View(Mapper.Map<StrategyViewModel>(_queryDispatcher.Execute(new StrategyByIdQuery(id))));
		}

		// POST: Strategy/Delete/5
		[HttpPost]
		public IActionResult Delete(Guid id, StrategyViewModel model)
		{
			try
			{
				_commandDispatcher.Execute(new StrategyRemoveCommand(id, model.OriginalVersion));

				return RedirectToAction("Index");
			}
			catch (DomainValidationException validationException)
			{
				ModelState.AddModelError(validationException.Property, validationException.Message);
			}

			return View(model);
		}

		/// <summary>
		/// Returns a list with all found strategy names
		/// </summary>
		/// <param name="term">Search text</param>
		/// <returns></returns>
		public IActionResult GetStrategyName(string term)
		{
			var items = _queryDispatcher.Execute(new StrategyNameSearchQuery(term));

			return Json(items.Take(10));
		}

		/// <summary>
		/// Get image by id
		/// </summary>
		/// <param name="id">Image ID</param>
		/// <returns>Image</returns>
		public IActionResult GetImage(Guid id)
		{
			var image = _queryDispatcher.Execute(new StrategyImageByIdQuery(id));

			return image == null ? null : new ImageResult(image.Data, image.ContentType);
		}
	}
}