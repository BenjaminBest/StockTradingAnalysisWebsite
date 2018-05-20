using StockTradingAnalysis.Domain.Events.Domain;
using StockTradingAnalysis.Domain.Events.Events;
using StockTradingAnalysis.Domain.Events.Exceptions;
using StockTradingAnalysis.Interfaces.Domain;
using StockTradingAnalysis.Interfaces.Events;
using StockTradingAnalysis.Interfaces.ReadModel;

namespace StockTradingAnalysis.Domain.Events.EventHandler
{
    /// <summary>
    /// The TransactionSplitOrderAddedEventHandler handles a split or reverse split event
    /// </summary>
    /// <seealso cref="Interfaces.Events.IEventHandler{TransactionSplitOrderAddedEvent}" />
    public class TransactionSplitOrderAddedEventHandler : IEventHandler<TransactionSplitOrderAddedEvent>
    {
        private readonly IModelRepository<ITransaction> _writerRepository;
        private readonly IModelReaderRepository<IStock> _stockRepository;
        private readonly ITransactionBook _transactionBook;

        /// <summary>
        /// Initializes this object
        /// </summary>
        /// <param name="modelRepository">The repository for reading and writing</param>
        /// <param name="stockRepository">The repository for reading stocks</param>
        /// <param name="transactionBook">The transaction book</param>
        public TransactionSplitOrderAddedEventHandler(
            IModelRepository<ITransaction> modelRepository,
            IModelReaderRepository<IStock> stockRepository,
            ITransactionBook transactionBook)
        {
            _writerRepository = modelRepository;
            _stockRepository = stockRepository;
            _transactionBook = transactionBook;
        }

        /// <summary>
        /// Processes the given event <paramref name="eventData"/>
        /// </summary>
        /// <param name="eventData">Event data</param>
        public void Handle(TransactionSplitOrderAddedEvent eventData)
        {
            if (_writerRepository.GetById(eventData.AggregateId) != null)
                return;

            var stock = _stockRepository.GetById(eventData.StockId);

            if (stock == null)
                throw new EventHandlerException($"No Stock found with id: {eventData.StockId}", typeof(TransactionSplitOrderAddedEventHandler));

            var item = new SplitTransaction(eventData.AggregateId)
            {
                OriginalVersion = eventData.Version,
                OrderDate = eventData.OrderDate,
                PricePerShare = eventData.PricePerShare,
                Stock = stock,
                Shares = eventData.Shares,
                OrderCosts = eventData.OrderCosts,
                Action = "Split/Reverse Split",
                PositionSize = eventData.PositionSize
            };

            _writerRepository.Add(item);


            //Add to transaction book
            _transactionBook.AddEntry(new SplitTransactionBookEntry(
                eventData.StockId,
                eventData.AggregateId,
                eventData.OrderDate,
                eventData.Shares,
                eventData.PricePerShare));
        }
    }
}