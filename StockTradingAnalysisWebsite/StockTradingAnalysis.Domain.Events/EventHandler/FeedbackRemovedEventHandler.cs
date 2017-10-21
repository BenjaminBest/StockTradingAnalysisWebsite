using StockTradingAnalysis.Domain.Events.Events;
using StockTradingAnalysis.Domain.Events.Exceptions;
using StockTradingAnalysis.Interfaces.Domain;
using StockTradingAnalysis.Interfaces.Events;
using StockTradingAnalysis.Interfaces.ReadModel;

namespace StockTradingAnalysis.Domain.Events.EventHandler
{
    public class FeedbackRemovedEventHandler : IEventHandler<FeedbackRemovedEvent>
    {
        private readonly IModelRepository<IFeedback> _writerRepository;
        private readonly IModelRepository<IFeedbackProportion> _feedbackProportionRepository;

        /// <summary>
        /// Initializes this object
        /// </summary>
        /// <param name="modelRepository">The repository for reading and writing</param>
        /// <param name="feedbackProportionRepository">The repository for reading and writing the feedback proportions</param>
        public FeedbackRemovedEventHandler(IModelRepository<IFeedback> modelRepository, IModelRepository<IFeedbackProportion> feedbackProportionRepository)
        {
            _writerRepository = modelRepository;
            _feedbackProportionRepository = feedbackProportionRepository;
        }

        /// <summary>
        /// Processes the given event <paramref name="eventData"/>
        /// </summary>
        /// <param name="eventData">Event data</param>
        public void Handle(FeedbackRemovedEvent eventData)
        {
            var item = _writerRepository.GetById(eventData.AggregateId);

            if (item == null)
                return;

            _writerRepository.Delete(item);

            //Proportion
            var proportion = _feedbackProportionRepository.GetById(eventData.AggregateId);

            if (proportion == null)
                throw new EventHandlerException($"No Feedback proportion found for id: {eventData.AggregateId}", typeof(FeedbackAddedEventHandler));

            _feedbackProportionRepository.Delete(proportion);
        }
    }
}