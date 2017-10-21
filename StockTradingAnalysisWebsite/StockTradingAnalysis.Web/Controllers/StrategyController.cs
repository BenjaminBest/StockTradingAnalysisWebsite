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
        public ActionResult Index()
        {
            return View(Mapper.Map<IEnumerable<StrategyViewModel>>(_queryDispatcher.Execute(new StrategyAllQuery())));
        }

        // GET: Strategy/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Strategy/Create
        [HttpPost]
        public ActionResult Create(StrategyViewModel model, FormCollection collection)
        {
            var id = Guid.NewGuid();

            try
            {
                IImage image = null;

                if (Request.Files["Image"] != null && Request.Files["Image"].ContentLength != 0)
                {
                    image = _imageService.GetImage(Request.Files["Image"], collection["image.Description"], id);
                }

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
        public ActionResult Edit(Guid id)
        {
            return View(Mapper.Map<StrategyViewModel>(_queryDispatcher.Execute(new StrategyByIdQuery(id))));
        }

        // POST: Strategy/Edit/5
        [HttpPost]
        public ActionResult Edit(Guid id, StrategyViewModel model, FormCollection collection)
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
        public ActionResult Delete(Guid id)
        {
            return View(Mapper.Map<StrategyViewModel>(_queryDispatcher.Execute(new StrategyByIdQuery(id))));
        }

        // POST: Strategy/Delete/5
        [HttpPost]
        public ActionResult Delete(Guid id, StrategyViewModel model)
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
        public JsonResult GetStrategyName(string term)
        {
            var items = _queryDispatcher.Execute(new StrategyNameSearchQuery(term));

            return Json(items.Take(10), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Get image by id
        /// </summary>
        /// <param name="id">Image ID</param>
        /// <returns>Image</returns>
        public ActionResult GetImage(Guid id)
        {
            var image = _queryDispatcher.Execute(new StrategyImageByIdQuery(id));

            return image == null ? null : new ImageResult(image.Data, image.ContentType);
        }
    }
}