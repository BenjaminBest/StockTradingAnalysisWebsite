using AutoMapper;
using StockTradingAnalysis.Domain.CQRS.Cmd.Commands;
using StockTradingAnalysis.Domain.CQRS.Cmd.Exceptions;
using StockTradingAnalysis.Domain.CQRS.Query.Queries;
using StockTradingAnalysis.Interfaces.Commands;
using StockTradingAnalysis.Interfaces.Queries;
using StockTradingAnalysis.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace StockTradingAnalysis.Web.Controllers
{
    public class FeedbackController : Controller
    {
        private readonly IQueryDispatcher _queryDispatcher;
        private readonly ICommandDispatcher _commandDispatcher;

        public FeedbackController(IQueryDispatcher queryDispatcher, ICommandDispatcher commandDispatcher)
        {
            _queryDispatcher = queryDispatcher;
            _commandDispatcher = commandDispatcher;
        }

        // GET: Feedback
        public ActionResult Index()
        {
            ViewBag.Proportion = Mapper.Map<IEnumerable<FeedbackProportionViewModel>>(
                _queryDispatcher.Execute(new FeedbackProportionAllQuery())).ToDictionary(f => f.Id, f => f);


            return View(Mapper.Map<IEnumerable<FeedbackViewModel>>(_queryDispatcher.Execute(new FeedbackAllQuery())));
        }

        // GET: Feedback/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Feedback/Create
        [HttpPost]
        public ActionResult Create(FeedbackViewModel model)
        {
            var id = Guid.NewGuid();

            try
            {
                _commandDispatcher.Execute(new FeedbackAddCommand(id, model.OriginalVersion, model.Name,
                    model.Description));

                return RedirectToAction("Index");
            }
            catch (DomainValidationException validationException)
            {
                ModelState.AddModelError(validationException.Property, validationException.Message);
            }

            return View(model);
        }

        // GET: Feedback/Edit/5
        public ActionResult Edit(Guid id)
        {
            return View(Mapper.Map<FeedbackViewModel>(_queryDispatcher.Execute(new FeedbackByIdQuery(id))));
        }

        // POST: Feedback/Edit/5
        [HttpPost]
        public ActionResult Edit(Guid id, FeedbackViewModel model)
        {
            try
            {
                _commandDispatcher.Execute(new FeedbackChangeCommand(id, model.OriginalVersion, model.Name,
                    model.Description));

                return RedirectToAction("Index");
            }
            catch (DomainValidationException validationException)
            {
                ModelState.AddModelError(validationException.Property, validationException.Message);
            }

            return View(model);
        }

        // GET: Feedback/Delete/5
        public ActionResult Delete(Guid id)
        {
            return View(Mapper.Map<FeedbackViewModel>(_queryDispatcher.Execute(new FeedbackByIdQuery(id))));
        }

        // POST: Feedback/Delete/5
        [HttpPost]
        public ActionResult Delete(Guid id, FeedbackViewModel model)
        {
            try
            {
                _commandDispatcher.Execute(new FeedbackRemoveCommand(id, model.OriginalVersion));

                return RedirectToAction("Index");
            }
            catch (DomainValidationException validationException)
            {
                ModelState.AddModelError(validationException.Property, validationException.Message);
            }

            return View(model);
        }

        /// <summary>
        /// Returns a list with all found feedback names
        /// </summary>
        /// <param name="term">Search text</param>
        /// <returns></returns>
        public JsonResult GetFeedbackName(string term)
        {
            var items = _queryDispatcher.Execute(new FeedbackNameSearchQuery(term));

            return Json(items.Take(10), JsonRequestBehavior.AllowGet);
        }
    }
}