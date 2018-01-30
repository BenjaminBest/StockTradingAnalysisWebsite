using System;
using System.Linq;
using StockTradingAnalysis.Domain.Events.Domain;
using StockTradingAnalysis.Domain.Events.Events;
using StockTradingAnalysis.Interfaces.Domain;
using StockTradingAnalysis.Interfaces.Events;
using StockTradingAnalysis.Interfaces.ReadModel;

namespace StockTradingAnalysis.Domain.Events.EventHandler
{
    public class AccountBalanceChangedEventHandler : IEventHandler<TransactionDividendCalculatedEvent>, IEventHandler<TransactionPerformanceCalculatedEvent>
    {
        private readonly IModelRepository<IAccountBalance> _modelRepository;
        private readonly IModelReaderRepository<ITransaction> _transactionRepository;

        /// <summary>
        /// Initializes this object
        /// </summary>
        /// <param name="modelRepository">The repository for reading and writing</param>
        /// <param name="transactionRepository">The transaction repositiory</param>
        public AccountBalanceChangedEventHandler(IModelRepository<IAccountBalance> modelRepository, IModelReaderRepository<ITransaction> transactionRepository)
        {
            _modelRepository = modelRepository;
            _transactionRepository = transactionRepository;
        }

        /// <summary>
        /// Processes the given event <paramref name="eventData"/>
        /// </summary>
        /// <param name="eventData">Event data</param>
        public void Handle(TransactionDividendCalculatedEvent eventData)
        {
            AddItem(eventData.AggregateId, eventData.ProfitAbsolute);
        }

        /// <summary>
        /// Processes the given event <paramref name="eventData"/>
        /// </summary>
        /// <param name="eventData">Event data</param>
        public void Handle(TransactionPerformanceCalculatedEvent eventData)
        {
            AddItem(eventData.AggregateId, eventData.ProfitAbsolute);
        }

        private void AddItem(Guid aggregateId, decimal profitAbsolute)
        {
            if (_modelRepository.GetById(aggregateId) != null)
                return;

            //Get date from transaction
            var balanceDate = _transactionRepository.GetById(aggregateId).OrderDate;

            //Get previous balance
            var lastBalance = _modelRepository.GetAll().OrderByDescending(a => a.Date).FirstOrDefault(a => a.Date < balanceDate);

            var item = new AccountBalance(aggregateId, lastBalance?.Balance + profitAbsolute ?? profitAbsolute, balanceDate);

            _modelRepository.Add(item);
        }
    }
}