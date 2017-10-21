using StockTradingAnalysis.Domain.Events.Events;
using StockTradingAnalysis.Interfaces.Domain;
using StockTradingAnalysis.Interfaces.Events;
using StockTradingAnalysis.Interfaces.ReadModel;

namespace StockTradingAnalysis.Domain.Events.EventHandler
{
    public class TransactionUndoEventHandler : IEventHandler<TransactionUndoEvent>
    {
        private readonly IModelRepository<ITransaction> _writerRepository;

        /// <summary>
        /// Initializes this object
        /// </summary>
        /// <param name="modelRepository">The repository for reading and writing</param>
        public TransactionUndoEventHandler(IModelRepository<ITransaction> modelRepository)
        {
            _writerRepository = modelRepository;
        }

        /// <summary>
        /// Processes the given event <paramref name="eventData"/>
        /// </summary>
        /// <param name="eventData">Event data</param>
        public void Handle(TransactionUndoEvent eventData)
        {
            var item = _writerRepository.GetById(eventData.AggregateId);

            if (item == null)
                return;

            _writerRepository.Delete(item);

            //TODO: Further processing, what must be done when a transaction was reverted?
            //TODO: Revert balance
        }
    }
}