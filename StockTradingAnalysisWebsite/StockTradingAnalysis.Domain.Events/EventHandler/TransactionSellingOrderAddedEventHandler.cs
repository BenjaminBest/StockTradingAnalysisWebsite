using StockTradingAnalysis.Domain.Events.Domain;
using StockTradingAnalysis.Domain.Events.Events;
using StockTradingAnalysis.Domain.Events.Exceptions;
using StockTradingAnalysis.Interfaces.Domain;
using StockTradingAnalysis.Interfaces.Events;
using StockTradingAnalysis.Interfaces.ReadModel;
using System.Linq;

namespace StockTradingAnalysis.Domain.Events.EventHandler
{
    public class TransactionSellingOrderAddedEventHandler : IEventHandler<TransactionSellingOrderAddedEvent>
    {
        private readonly IModelRepository<ITransaction> _writerRepository;
        private readonly IModelReaderRepository<IStock> _stockRepository;
        private readonly IModelReaderRepository<IFeedback> _feedbackRepository;
        private readonly IModelReaderRepository<IFeedbackProportion> _feedbackProportionRepository;
        private readonly ITransactionBook _transactionBook;

        /// <summary>
        /// Initializes this object
        /// </summary>
        /// <param name="modelRepository">The repository for reading and writing</param>
        /// <param name="stockRepository">The repository for reading stocks</param>
        /// <param name="feedbackRepository">The repository for reading feedbacks</param>
        /// <param name="transactionBook">The transaction book</param>
        /// <param name="feedbackProportionRepository">The repository for reading the feedback proportions</param>
        public TransactionSellingOrderAddedEventHandler(
            IModelRepository<ITransaction> modelRepository,
            IModelReaderRepository<IStock> stockRepository,
            IModelReaderRepository<IFeedback> feedbackRepository,
            ITransactionBook transactionBook,
            IModelReaderRepository<IFeedbackProportion> feedbackProportionRepository)
        {
            _writerRepository = modelRepository;
            _stockRepository = stockRepository;
            _feedbackRepository = feedbackRepository;
            _transactionBook = transactionBook;
            _feedbackProportionRepository = feedbackProportionRepository;
        }

        /// <summary>
        /// Processes the given event <paramref name="eventData"/>
        /// </summary>
        /// <param name="eventData">Event data</param>
        public void Handle(TransactionSellingOrderAddedEvent eventData)
        {
            if (_writerRepository.GetById(eventData.AggregateId) != null)
                return;

            var stock = _stockRepository.GetById(eventData.StockId);

            if (stock == null)
                throw new EventHandlerException($"No Stock found with id: {eventData.StockId}", typeof(TransactionSellingOrderAddedEventHandler));

            var feedbacks = _feedbackRepository.GetAll().Where(f => eventData.Feedback.Contains(f.Id)).ToList();

            if (feedbacks == null)
                throw new EventHandlerException("No Feedback found for the given ids", typeof(TransactionSellingOrderAddedEventHandler));

            var item = new SellingTransaction(eventData.AggregateId)
            {
                OriginalVersion = eventData.Version,
                Description = eventData.Description,
                Image = eventData.Image,
                Feedback = feedbacks,
                MAE = eventData.MAE,
                MFE = eventData.MFE,
                OrderCosts = eventData.OrderCosts,
                OrderDate = eventData.OrderDate,
                PricePerShare = eventData.PricePerShare,
                Stock = stock,
                Tag = eventData.Tag,
                Taxes = eventData.Taxes,
                Shares = eventData.Shares,
                Action = "Verkauf",
                PositionSize = eventData.PositionSize
            };

            _writerRepository.Add(item);

            //Add to transaction book
            _transactionBook.AddEntry(new SellingTransactionBookEntry(
                eventData.StockId,
                eventData.AggregateId,
                eventData.OrderDate,
                eventData.Shares,
                eventData.PricePerShare,
                eventData.OrderCosts,
                eventData.Taxes));

            //Feedback Proportions
            var overallFeedbackAmount = _writerRepository.GetAll()
                .Where(t => t is ISellingTransaction).Cast<ISellingTransaction>()
                .Sum(transaction => transaction.Feedback.Count());


            foreach (var feedback in feedbacks)
            {
                var proportion = _feedbackProportionRepository.GetById(feedback.Id);

                proportion.FeedbackShare = _writerRepository
                    .GetAll().Where(t => t is ISellingTransaction)
                    .Cast<ISellingTransaction>()
                    .SelectMany(t => t.Feedback)
                    .Count(t => t.Id == feedback.Id);
            }

            foreach (var proportion in _feedbackProportionRepository.GetAll())
            {
                proportion.OverallFeedbackAmount = overallFeedbackAmount;
            }
        }
    }
}