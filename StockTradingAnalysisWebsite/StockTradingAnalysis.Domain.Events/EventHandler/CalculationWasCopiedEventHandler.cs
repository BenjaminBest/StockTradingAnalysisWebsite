using StockTradingAnalysis.Domain.Events.Domain;
using StockTradingAnalysis.Domain.Events.Events;
using StockTradingAnalysis.Interfaces.Domain;
using StockTradingAnalysis.Interfaces.Events;
using StockTradingAnalysis.Interfaces.ReadModel;

namespace StockTradingAnalysis.Domain.Events.EventHandler
{
    /// <summary>
    /// The vent handler CalculationWasCopiedEventHandler is invoked when a calculation was copied.
    /// </summary>
    /// <seealso cref="Interfaces.Events.IEventHandler{CalculationWasCopiedEvent}" />
    public class CalculationWasCopiedEventHandler : IEventHandler<CalculationWasCopiedEvent>
    {
        /// <summary>
        /// The writer repository
        /// </summary>
        private readonly IModelRepository<ICalculation> _writerRepository;

        /// <summary>
        /// Initializes this object
        /// </summary>
        /// <param name="modelRepository">The repository for reading and writing</param>
        public CalculationWasCopiedEventHandler(IModelRepository<ICalculation> modelRepository)
        {
            _writerRepository = modelRepository;
        }

        /// <summary>
        /// Processes the given event <paramref name="eventData"/>
        /// </summary>
        /// <param name="eventData">Event data</param>
        public void Handle(CalculationWasCopiedEvent eventData)
        {
            var newItem = new Calculation(eventData.AggregateId)
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

            _writerRepository.Add(newItem);
        }
    }
}