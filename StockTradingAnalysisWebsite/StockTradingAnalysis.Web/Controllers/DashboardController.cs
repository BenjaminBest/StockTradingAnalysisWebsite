using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using StockTradingAnalysis.Domain.CQRS.Query.Queries;
using StockTradingAnalysis.Interfaces.Queries;
using StockTradingAnalysis.Interfaces.Services.Domain;
using StockTradingAnalysis.Web.Models;

namespace StockTradingAnalysis.Web.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IQueryDispatcher _queryDispatcher;
        private readonly IAccumulationPlanStatisticService _accumulationPlanStatisticService;

        public DashboardController(
            IQueryDispatcher queryDispatcher,
            IAccumulationPlanStatisticService accumulationPlanStatisticService)
        {
            _queryDispatcher = queryDispatcher;
            _accumulationPlanStatisticService = accumulationPlanStatisticService;
        }

        // GET: Dashboard
        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Dashboard/SavingsPlan
        public ViewResult SavingsPlan()
        {
            var model = new List<SavingsPlanViewModel>();

            foreach (var tag in _queryDispatcher.Execute(new TransactionTagAllQuery()))
            {
                if (string.IsNullOrEmpty(tag))
                    continue;

                model.Add(Mapper.Map<SavingsPlanViewModel>(_accumulationPlanStatisticService.CalculateSavingsPlan(tag)));
            }

            return View(model);
        }
    }
}