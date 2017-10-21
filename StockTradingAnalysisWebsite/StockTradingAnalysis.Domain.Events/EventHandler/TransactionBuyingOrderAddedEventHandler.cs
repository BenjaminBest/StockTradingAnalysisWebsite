using StockTradingAnalysis.Domain.Events.Domain;
using StockTradingAnalysis.Domain.Events.Events;
using StockTradingAnalysis.Domain.Events.Exceptions;
using StockTradingAnalysis.Interfaces.Domain;
using StockTradingAnalysis.Interfaces.Events;
using StockTradingAnalysis.Interfaces.ReadModel;

namespace StockTradingAnalysis.Domain.Events.EventHandler
{
    public class TransactionBuyingOrderAddedEventHandler : IEventHandler<TransactionBuyingOrderAddedEvent>
    {
        private readonly IModelRepository<ITransaction> _writerRepository;
        private readonly IModelReaderRepository<IStock> _stockRepository;
        private readonly IModelReaderRepository<IStrategy> _strategyRepository;
        private readonly ITransactionBook _transactionBook;

        /// <summary>
        /// Initializes this object
        /// </summary>
        /// <param name="modelRepository">The repository for reading and writing</param>
        /// <param name="stockRepository">The repository for reading stocks</param>
        /// <param name="strategyRepository">The repository for reading strategies</param>
        /// <param name="transactionBook">The transaction book</param>
        public TransactionBuyingOrderAddedEventHandler(
            IModelRepository<ITransaction> modelRepository,
            IModelReaderRepository<IStock> stockRepository,
            IModelReaderRepository<IStrategy> strategyRepository,
            ITransactionBook transactionBook)
        {
            _writerRepository = modelRepository;
            _stockRepository = stockRepository;
            _strategyRepository = strategyRepository;
            _transactionBook = transactionBook;
        }

        /// <summary>
        /// Processes the given event <paramref name="eventData"/>
        /// </summary>
        /// <param name="eventData">Event data</param>
        public void Handle(TransactionBuyingOrderAddedEvent eventData)
        {
            if (_writerRepository.GetById(eventData.AggregateId) != null)
                return;

            var stock = _stockRepository.GetById(eventData.StockId);

            if (stock == null)
                throw new EventHandlerException($"No Stock found with id: {eventData.StockId}", typeof(TransactionBuyingOrderAddedEventHandler));

            var strategy = _strategyRepository.GetById(eventData.StrategyId);

            if (strategy == null)
                throw new EventHandlerException($"No Strategy found with id: {eventData.StrategyId}", typeof(TransactionBuyingOrderAddedEventHandler));

            var item = new BuyingTransaction(eventData.AggregateId)
            {
                OriginalVersion = eventData.Version,
                Description = eventData.Description,
                Image = eventData.Image,
                InitialSL = eventData.InitialSL,
                InitialTP = eventData.InitialTP,
                OrderCosts = eventData.OrderCosts,
                OrderDate = eventData.OrderDate,
                PricePerShare = eventData.PricePerShare,
                Stock = stock,
                Strategy = strategy,
                Tag = eventData.Tag,
                Shares = eventData.Shares,
                Action = "Kauf",
                PositionSize = eventData.PositionSize,
                CRV = eventData.CRV
            };

            _writerRepository.Add(item);

            //Add to transaction book
            _transactionBook.AddEntry(new BuyingTransactionBookEntry(
                eventData.StockId,
                eventData.AggregateId,
                eventData.OrderDate,
                eventData.Shares,
                eventData.PricePerShare,
                eventData.OrderCosts));
        }
    }
}