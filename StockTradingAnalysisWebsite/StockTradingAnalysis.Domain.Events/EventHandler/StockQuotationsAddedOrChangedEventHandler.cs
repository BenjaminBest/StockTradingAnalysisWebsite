using StockTradingAnalysis.Domain.Events.Domain;
using StockTradingAnalysis.Domain.Events.Events;
using StockTradingAnalysis.Interfaces.Domain;
using StockTradingAnalysis.Interfaces.Events;
using StockTradingAnalysis.Interfaces.ReadModel;

namespace StockTradingAnalysis.Domain.Events.EventHandler
{
    /// <summary>
    /// The StockQuotationsAddedOrChangedEventHandler update the quotations of a stock.
    /// </summary>
    /// <seealso cref="Interfaces.Events.IEventHandler{StockQuotationsAddedOrChangedEvent}" />
    public class StockQuotationsAddedOrChangedEventHandler : IEventHandler<StockQuotationsAddedOrChangedEvent>
    {
        /// <summary>
        /// The writer repository
        /// </summary>
        private readonly IModelRepository<IStock> _writerRepository;

        /// <summary>
        /// Initializes this object
        /// </summary>
        /// <param name="modelRepository">The repository for reading and writing</param>
        public StockQuotationsAddedOrChangedEventHandler(IModelRepository<IStock> modelRepository)
        {
            _writerRepository = modelRepository;
        }

        /// <summary>
        /// Processes the given event <paramref name="eventData"/>
        /// </summary>
        /// <param name="eventData">Event data</param>
        public void Handle(StockQuotationsAddedOrChangedEvent eventData)
        {
            var item = _writerRepository.GetById(eventData.AggregateId);

            if (item == null)
                return;

            foreach (var quotation in eventData.Quotations)
            {
                ((Stock)item).AddQuotation(quotation);
            }

            item.OriginalVersion = eventData.Version;
            _writerRepository.Update(item);
        }
    }
}