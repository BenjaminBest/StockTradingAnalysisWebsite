using StockTradingAnalysis.Domain.Events.Domain;
using StockTradingAnalysis.Domain.Events.Events;
using StockTradingAnalysis.Interfaces.Domain;
using StockTradingAnalysis.Interfaces.Events;
using StockTradingAnalysis.Interfaces.ReadModel;

namespace StockTradingAnalysis.Domain.Events.EventHandler
{
    public class CalculationDescriptionChangedEventHandler : IEventHandler<CalculationDescriptionChangedEvent>
    {
        private readonly IModelRepository<ICalculation> _writerRepository;

        /// <summary>
        /// Initializes this object
        /// </summary>
        /// <param name="modelRepository">The repository for reading and writing</param>
        public CalculationDescriptionChangedEventHandler(IModelRepository<ICalculation> modelRepository)
        {
            _writerRepository = modelRepository;
        }

        /// <summary>
        /// Processes the given event <paramref name="eventData"/>
        /// </summary>
        /// <param name="eventData">Event data</param>
        public void Handle(CalculationDescriptionChangedEvent eventData)
        {
            var item = _writerRepository.GetById(eventData.AggregateId);

            if (item == null)
                return;

            ((Calculation) item).Description = eventData.Description;
            item.OriginalVersion = eventData.Version;

            _writerRepository.Update(item);
        }
    }
}