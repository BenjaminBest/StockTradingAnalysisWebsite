using System;
using System.Linq;
using StockTradingAnalysis.Domain.Events.Domain;
using StockTradingAnalysis.Domain.Events.Events;
using StockTradingAnalysis.Interfaces.Domain;
using StockTradingAnalysis.Interfaces.Events;
using StockTradingAnalysis.Interfaces.ReadModel;

namespace StockTradingAnalysis.Domain.Events.EventHandler
{
    /// <summary>
    /// The AccountBalanceChangedEventHandler calculates the overall balance of the account based on the profit for a new transaction. All future balances
    /// needs to be re-calculated then.
    /// </summary>
    /// <seealso cref="Interfaces.Events.IEventHandler{TransactionDividendCalculatedEvent}" />
    /// <seealso cref="Interfaces.Events.IEventHandler{TransactionPerformanceCalculatedEvent}" />
    public class AccountBalanceChangedEventHandler : IEventHandler<TransactionDividendCalculatedEvent>,
        IEventHandler<TransactionPerformanceCalculatedEvent>
    {
        private readonly IModelRepository<IAccountBalance> _modelRepository;
        private readonly IModelReaderRepository<ITransaction> _transactionRepository;

        /// <summary>
        /// Initializes this object
        /// </summary>
        /// <param name="modelRepository">The repository for reading and writing</param>
        /// <param name="transactionRepository">The transaction repositiory</param>
        public AccountBalanceChangedEventHandler(IModelRepository<IAccountBalance> modelRepository,
            IModelReaderRepository<ITransaction> transactionRepository)
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

        /// <summary>
        /// Adds the item.
        /// </summary>
        /// <param name="aggregateId">The aggregate identifier.</param>
        /// <param name="profitAbsolute">The profit absolute.</param>
        private void AddItem(Guid aggregateId, decimal profitAbsolute)
        {
            if (_modelRepository.GetById(aggregateId) != null)
                return;

            //Get date from transaction
            var balanceDate = _transactionRepository.GetById(aggregateId).OrderDate;

            //Get previous balance
            var lastBalance = _modelRepository.GetAll().OrderByDescending(a => a.Date).FirstOrDefault(a => a.Date < balanceDate);

            //Add new balance
            var item = new AccountBalance(aggregateId, lastBalance?.Balance + profitAbsolute ?? profitAbsolute, profitAbsolute, balanceDate);
            _modelRepository.Add(item);

            //Get future balances
            var futureAccountBalances = _modelRepository.GetAll().OrderBy(a => a.Date).Where(a => a.Date > balanceDate);

            using (var enumerator = futureAccountBalances.GetEnumerator())
            {
                var oldBalance = item.Balance;
                while (enumerator.MoveNext())
                {
                    var newBalance = new AccountBalance(
                        enumerator.Current.TransactionId,
                        oldBalance + enumerator.Current.BalanceChange,
                        enumerator.Current.BalanceChange,
                        enumerator.Current.Date);

                    _modelRepository.Update(newBalance);

                    oldBalance = newBalance.Balance;
                }
            }
        }
    }
}