using StockTradingAnalysis.Domain.Events.Domain;
using StockTradingAnalysis.Domain.Events.Events;
using StockTradingAnalysis.Domain.Events.Exceptions;
using StockTradingAnalysis.Interfaces.Domain;
using StockTradingAnalysis.Interfaces.Events;
using StockTradingAnalysis.Interfaces.ReadModel;
using System.Linq;

namespace StockTradingAnalysis.Domain.Events.EventHandler
{
    public class FeedbackAddedEventHandler : IEventHandler<FeedbackAddedEvent>
    {
        private readonly IModelRepository<IFeedback> _writerRepository;
        private readonly IModelRepository<IFeedbackProportion> _feedbackProportionRepository;
        private readonly IModelReaderRepository<ITransaction> _transactionRepository;

        /// <summary>
        /// Initializes this object
        /// </summary>
        /// <param name="modelRepository">The repository for reading and writing</param>
        /// <param name="feedbackProportionRepository">The repository for reading and writing the feedback proportions</param>
        /// <param name="transactionRepository">The repository for reading the transactions</param>
        public FeedbackAddedEventHandler(
            IModelRepository<IFeedback> modelRepository,
            IModelRepository<IFeedbackProportion> feedbackProportionRepository,
            IModelReaderRepository<ITransaction> transactionRepository)
        {
            _writerRepository = modelRepository;
            _feedbackProportionRepository = feedbackProportionRepository;
            _transactionRepository = transactionRepository;
        }

        /// <summary>
        /// Processes the given event <paramref name="eventData"/>
        /// </summary>
        /// <param name="eventData">Event data</param>
        public void Handle(FeedbackAddedEvent eventData)
        {
            if (_writerRepository.GetById(eventData.AggregateId) != null)
                return;

            var item = new Feedback(eventData.AggregateId)
            {
                Name = eventData.Name,
                Description = eventData.Description,
                OriginalVersion = eventData.Version
            };

            _writerRepository.Add(item);

            //Proportion
            if (_feedbackProportionRepository.GetById(eventData.AggregateId) != null)
                throw new EventHandlerException($"Feedback proportion with id: {eventData.AggregateId} shouldn't exist", typeof(FeedbackAddedEventHandler));

            var overallFeedbackAmount = _transactionRepository.GetAll()
                .Where(t => t is ISellingTransaction).Cast<ISellingTransaction>()
                .Sum(transaction => transaction.Feedback.Count());

            var proportion = new FeedbackProportion(eventData.AggregateId) { FeedbackShare = 0, OverallFeedbackAmount = overallFeedbackAmount };

            _feedbackProportionRepository.Add(proportion);

        }
    }
}