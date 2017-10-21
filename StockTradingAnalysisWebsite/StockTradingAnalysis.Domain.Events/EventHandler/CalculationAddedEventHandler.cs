using StockTradingAnalysis.Domain.Events.Domain;
using StockTradingAnalysis.Domain.Events.Events;
using StockTradingAnalysis.Interfaces.Domain;
using StockTradingAnalysis.Interfaces.Events;
using StockTradingAnalysis.Interfaces.ReadModel;

namespace StockTradingAnalysis.Domain.Events.EventHandler
{
    public class CalculationAddedEventHandler : IEventHandler<CalculationAddedEvent>
    {
        private readonly IModelRepository<ICalculation> _writerRepository;

        /// <summary>
        /// Initializes this object
        /// </summary>
        /// <param name="modelRepository">The repository for reading and writing</param>
        public CalculationAddedEventHandler(IModelRepository<ICalculation> modelRepository)
        {
            _writerRepository = modelRepository;
        }

        /// <summary>
        /// Processes the given event <paramref name="eventData"/>
        /// </summary>
        /// <param name="eventData">Event data</param>
        public void Handle(CalculationAddedEvent eventData)
        {
            if (_writerRepository.GetById(eventData.AggregateId) != null)
                return;

            var item = new Calculation(eventData.AggregateId)
            {
                Name = eventData.Name,
                Wkn = eventData.Wkn,
                Multiplier = eventData.Multiplier,
                StrikePrice = eventData.StrikePrice,
                Underlying = eventData.Underlying,
                InitialSl = eventData.InitialSl,
                InitialTp = eventData.InitialTp,
                PricePerUnit = eventData.PricePerUnit,
                OrderCosts = eventData.OrderCosts,
                Description = eventData.Description,
                Units = eventData.Units,
                IsLong = eventData.IsLong,
                OriginalVersion = eventData.Version
            };

            _writerRepository.Add(item);
        }
    }
}