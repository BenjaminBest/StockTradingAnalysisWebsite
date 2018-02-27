using StockTradingAnalysis.Domain.Events.Domain;
using StockTradingAnalysis.Domain.Events.Events;
using StockTradingAnalysis.Interfaces.Domain;
using StockTradingAnalysis.Interfaces.Events;
using StockTradingAnalysis.Interfaces.ReadModel;

namespace StockTradingAnalysis.Domain.Events.EventHandler
{
    /// <summary>
    /// The StockQuotationsChangedEventHandler adds a quotation to a stock.
    /// </summary>
    /// <seealso cref="Interfaces.Events.IEventHandler{StockQuotationAddedEvent}" />
    public class StockQuotationsAddedEventHandler : IEventHandler<StockQuotationAddedEvent>
    {
        /// <summary>
        /// The writer repository
        /// </summary>
        private readonly IModelRepository<IStock> _writerRepository;

        /// <summary>
        /// Initializes this object
        /// </summary>
        /// <param name="modelRepository">The repository for reading and writing</param>
        public StockQuotationsAddedEventHandler(IModelRepository<IStock> modelRepository)
        {
            _writerRepository = modelRepository;
        }

        /// <summary>
        /// Processes the given event <paramref name="eventData"/>
        /// </summary>
        /// <param name="eventData">Event data</param>
        public void Handle(StockQuotationAddedEvent eventData)
        {
            var item = _writerRepository.GetById(eventData.AggregateId);

            if (item == null)
                return;

            ((Stock)item).AddQuotation(eventData.Quotation);
            item.OriginalVersion = eventData.Version;

            _writerRepository.Update(item);
        }
    }
}