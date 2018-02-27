using System.Threading.Tasks;
using StockTradingAnalysis.Core.Common;
using StockTradingAnalysis.Domain.Events.Events;
using StockTradingAnalysis.Interfaces.Common;
using StockTradingAnalysis.Interfaces.Domain;
using StockTradingAnalysis.Interfaces.Events;
using StockTradingAnalysis.Interfaces.ReadModel;
using StockTradingAnalysis.Interfaces.Services.Domain;

namespace StockTradingAnalysis.Domain.Events.EventHandler
{
    /// <summary>
    /// The eventhandler StaticsticsBasicDataChangedEventHandler is involved when the underlaying basic data for the statistics have changed and
    /// a re-calculated is needed.
    /// </summary>
    /// <seealso cref="Interfaces.Events.IEventHandler{StaticsticsBasicDataChangedEvent}" />
    public class StaticsticsBasicDataChangedEventHandler : IEventHandler<StaticsticsBasicDataChangedEvent>
    {
        /// <summary>
        /// The model repository
        /// </summary>
        private readonly ITimeSliceModelRepository<IStatistic> _modelRepository;

        /// <summary>
        /// The time slice creation service
        /// </summary>
        private readonly ITimeSliceCreationService _timeSliceCreationService;

        /// <summary>
        /// The logging service
        /// </summary>
        private readonly ILoggingService _loggingService;

        /// <summary>
        /// The statistic service
        /// </summary>
        private readonly IStatisticService _statisticService;

        /// <summary>
        /// Initializes a new instance of the <see cref="StaticsticsBasicDataChangedEventHandler" /> class.
        /// </summary>
        /// <param name="modelRepository">The model repository.</param>
        /// <param name="timeSliceCreationService">The time slice creation service.</param>
        /// <param name="loggingService">The logging service.</param>
        /// <param name="statisticService">The statistic service.</param>
        public StaticsticsBasicDataChangedEventHandler(
            ITimeSliceModelRepository<IStatistic> modelRepository,
            ITimeSliceCreationService timeSliceCreationService,
            ILoggingService loggingService,
            IStatisticService statisticService)
        {
            _modelRepository = modelRepository;
            _timeSliceCreationService = timeSliceCreationService;
            _loggingService = loggingService;
            _statisticService = statisticService;
        }

        /// <summary>
        /// Processes the given event <paramref name="eventData"/>
        /// </summary>
        /// <param name="eventData">Event data</param>
        public void Handle(StaticsticsBasicDataChangedEvent eventData)
        {
            var slices = _timeSliceCreationService.CreateTimeSlices(eventData.Date);

            //Re-calculate the statistics for all slices in parallel, because they to not overlap. The items in the repository
            //are therefore accessed in a non-blocking way.
            Parallel.ForEach(slices.GetAllSlices(),
                slice =>
                {
                    using (new TimeMeasure(ms => _loggingService.Debug($"Re-Calculate statistics for time range: {slice} in {ms} msec")))
                    {
                        var calculationResult = _statisticService.Calculate(slices);

                        var item = _modelRepository.GetById(calculationResult);

                        if (item != null)
                        {
                            _modelRepository.Update(calculationResult);
                        }
                        else
                        {
                            _modelRepository.Add(calculationResult);
                        }
                    }
                });
        }
    }
}