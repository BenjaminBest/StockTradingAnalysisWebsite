using StockTradingAnalysis.Domain.Events.Domain;
using StockTradingAnalysis.Domain.Events.Events;
using StockTradingAnalysis.Interfaces.Domain;
using StockTradingAnalysis.Interfaces.Events;
using StockTradingAnalysis.Interfaces.ReadModel;

namespace StockTradingAnalysis.Domain.Events.EventHandler
{
    public class StrategyAddedEventHandler : IEventHandler<StrategyAddedEvent>
    {
        private readonly IModelRepository<IStrategy> _writerRepository;

        /// <summary>
        /// Initializes this object
        /// </summary>
        /// <param name="modelRepository">The repository for reading and writing</param>
        public StrategyAddedEventHandler(IModelRepository<IStrategy> modelRepository)
        {
            _writerRepository = modelRepository;
        }

        /// <summary>
        /// Processes the given event <paramref name="eventData"/>
        /// </summary>
        /// <param name="eventData">Event data</param>
        public void Handle(StrategyAddedEvent eventData)
        {
            if (_writerRepository.GetById(eventData.AggregateId) != null)
                return;

            var item = new Strategy(eventData.AggregateId)
            {
                Name = eventData.Name,
                Description = eventData.Description,
                OriginalVersion = eventData.Version,
                Image = eventData.Image
            };

            _writerRepository.Add(item);
        }
    }
}