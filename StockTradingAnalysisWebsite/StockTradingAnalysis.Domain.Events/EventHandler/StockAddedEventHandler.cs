using StockTradingAnalysis.Domain.Events.Domain;
using StockTradingAnalysis.Domain.Events.Events;
using StockTradingAnalysis.Interfaces.Domain;
using StockTradingAnalysis.Interfaces.Events;
using StockTradingAnalysis.Interfaces.ReadModel;

namespace StockTradingAnalysis.Domain.Events.EventHandler
{
    public class StockAddedEventHandler : IEventHandler<StockAddedEvent>
    {
        private readonly IModelRepository<IStock> _writerRepository;

        /// <summary>
        /// Initializes this object
        /// </summary>
        /// <param name="modelRepository">The repository for reading and writing</param>
        public StockAddedEventHandler(IModelRepository<IStock> modelRepository)
        {
            _writerRepository = modelRepository;
        }

        /// <summary>
        /// Processes the given event <paramref name="eventData"/>
        /// </summary>
        /// <param name="eventData">Event data</param>
        public void Handle(StockAddedEvent eventData)
        {
            if (_writerRepository.GetById(eventData.AggregateId) != null)
                return;

            var item = new Stock(eventData.AggregateId)
            {
                Name = eventData.Name,
                Wkn = eventData.Wkn,
                Type = eventData.Type,
                LongShort = eventData.LongShort,
                OriginalVersion = eventData.Version
            };

            _writerRepository.Add(item);
        }
    }
}