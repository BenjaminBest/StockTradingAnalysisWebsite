using StockTradingAnalysis.Domain.Events.Domain;
using StockTradingAnalysis.Domain.Events.Events;
using StockTradingAnalysis.Interfaces.Domain;
using StockTradingAnalysis.Interfaces.Events;
using StockTradingAnalysis.Interfaces.ReadModel;

namespace StockTradingAnalysis.Domain.Events.EventHandler
{
    public class StockOverallPerformanceChangedEventHandler : IEventHandler<StockOverallPerformanceChangedEvent>
    {
        private readonly IModelRepository<IStockStatistics> _writerRepository;

        /// <summary>
        /// Initializes this object
        /// </summary>
        /// <param name="modelRepository">The repository for reading and writing</param>
        public StockOverallPerformanceChangedEventHandler(IModelRepository<IStockStatistics> modelRepository)
        {
            _writerRepository = modelRepository;
        }

        /// <summary>
        /// Processes the given event <paramref name="eventData"/>
        /// </summary>
        /// <param name="eventData">Event data</param>
        public void Handle(StockOverallPerformanceChangedEvent eventData)
        {
            var item = _writerRepository.GetById(eventData.StockId);

            if (item == null)
            {
                _writerRepository.Add(new StockStatistics(eventData.StockId, eventData.NewProfitAbsolute));
                return;
            }

            ((StockStatistics)item).Performance += eventData.NewProfitAbsolute;
            _writerRepository.Update(item);
        }
    }
}