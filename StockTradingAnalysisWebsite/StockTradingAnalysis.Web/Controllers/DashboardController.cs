using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using StockTradingAnalysis.Domain.CQRS.Query.Queries;
using StockTradingAnalysis.Interfaces.Queries;
using StockTradingAnalysis.Interfaces.Services.Domain;
using StockTradingAnalysis.Web.Common.Interfaces;
using StockTradingAnalysis.Web.Models;

namespace StockTradingAnalysis.Web.Controllers
{
    /// <summary>
    /// The DashboardController gathers all information which is needed to render a dashboard with KPIs.
    /// </summary>
    /// <seealso cref="System.Web.Mvc.Controller" />
    public class DashboardController : Controller
    {
        /// <summary>
        /// The query dispatcher
        /// </summary>
        private readonly IQueryDispatcher _queryDispatcher;

        /// <summary>
        /// The accumulation plan statistic service
        /// </summary>
        private readonly IAccumulationPlanStatisticService _accumulationPlanStatisticService;

        /// <summary>
        /// The time slice creation service
        /// </summary>
        private readonly ITimeSliceCreationService _timeSliceCreationService;

        /// <summary>
        /// The statistic card converter repository
        /// </summary>
        private readonly IStatisticCardConverterRepository _statisticCardConverterRepository;

        public DashboardController(
            IQueryDispatcher queryDispatcher,
            IAccumulationPlanStatisticService accumulationPlanStatisticService,
            ITimeSliceCreationService timeSliceCreationService,
            IStatisticCardConverterRepository statisticCardConverterRepository)
        {
            _queryDispatcher = queryDispatcher;
            _accumulationPlanStatisticService = accumulationPlanStatisticService;
            _timeSliceCreationService = timeSliceCreationService;
            _statisticCardConverterRepository = statisticCardConverterRepository;
        }

        // GET: Dashboard
        public ActionResult Index()
        {
            var model = new DashboardViewModel();

            var statistic =
                _queryDispatcher.Execute(new StatisticsByTimeSliceQuery(_timeSliceCreationService.CreateTimeSlices()));

            model.Cards = _statisticCardConverterRepository.ConvertStatistic(statistic);

            return View(model);
        }

        //
        // GET: /Dashboard/SavingsPlan
        public ActionResult SavingsPlan()
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