using StockTradingAnalysis.Domain.Events.Domain;
using StockTradingAnalysis.Domain.Events.Events;
using StockTradingAnalysis.Interfaces.Domain;
using StockTradingAnalysis.Interfaces.Events;
using StockTradingAnalysis.Interfaces.ReadModel;

namespace StockTradingAnalysis.Domain.Events.EventHandler
{
    public class TransactionDividendCalculatedEventHandler : IEventHandler<TransactionDividendCalculatedEvent>
    {
        private readonly IModelRepository<ITransactionPerformance> _writerRepository;

        /// <summary>
        /// Initializes this object
        /// </summary>
        /// <param name="modelRepository">The repository for reading and writing</param>
        public TransactionDividendCalculatedEventHandler(IModelRepository<ITransactionPerformance> modelRepository)
        {
            _writerRepository = modelRepository;
        }

        /// <summary>
        /// Processes the given event <paramref name="eventData"/>
        /// </summary>
        /// <param name="eventData">Event data</param>
        public void Handle(TransactionDividendCalculatedEvent eventData)
        {
            var item = new TransactionPerformance(
                eventData.AggregateId,
                eventData.ProfitAbsolute,
                eventData.ProfitPercentage,
                eventData.HoldingPeriod.StartDate,
                eventData.HoldingPeriod.EndDate,
                eventData.R);

            if (_writerRepository.GetById(eventData.AggregateId) == null)
            {
                _writerRepository.Add(item);
            }
            else
            {
                _writerRepository.Update(item);
            }
        }
    }
}