using System;
using System.Linq;
using StockTradingAnalysis.Domain.Events.Aggregates;
using StockTradingAnalysis.Domain.Events.Events;
using StockTradingAnalysis.Domain.Process.Data;
using StockTradingAnalysis.Interfaces.Domain;
using StockTradingAnalysis.Interfaces.Events;
using StockTradingAnalysis.Interfaces.Services.Domain;

namespace StockTradingAnalysis.Domain.Process.ProcessManagers
{
    /// <summary>
    /// The class TransactionProcessManager manages the process of a transaction.
    /// </summary>
    /// <seealso cref="ProcessManagerBase{TData}" />
    /// <seealso cref="IProcessManager" />
    public class TransactionProcessManager : ProcessManagerBase<TransactionProcessManagerData>,
        IStartedByMessage<TransactionDividendOrderAddedEvent>,
        IStartedByMessage<TransactionSellingOrderAddedEvent>
    {
        private readonly ITransactionPerformanceService _transactionPerformanceService;
        private readonly ITransactionBook _transactionBook;
        private readonly IEventBus _eventBus;

        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionProcessManager"/> class.
        /// </summary>
        /// <param name="transactionPerformanceService">The transaction performance service</param>
        /// <param name="transactionBook">The transaction book.</param>
        /// <param name="eventBus"></param>
        public TransactionProcessManager(
            ITransactionPerformanceService transactionPerformanceService,
            ITransactionBook transactionBook,
            IEventBus eventBus)
        {
            _transactionPerformanceService = transactionPerformanceService;
            _transactionBook = transactionBook;
            _eventBus = eventBus;
        }

        /// <summary>
        /// Handles the given message <paramref name="message" />
        /// </summary>
        /// <param name="message">The message.</param>
        public void Handle(TransactionDividendOrderAddedEvent message)
        {
            CalculateDividendPerformance(message.AggregateId, message.StockId);
            StatusUpdate.MarkAsCompleted(this);
        }

        /// <summary>
        /// Handles the given message <paramref name="message" />
        /// </summary>
        /// <param name="message">The message.</param>
        public void Handle(TransactionSellingOrderAddedEvent message)
        {
            CalculatePerformance(message.AggregateId, message.StockId, message.MAE, message.MFE);
            StatusUpdate.MarkAsCompleted(this);
        }

        /// <summary>
        /// Calculates the performance for this transaction if its a selling transaction
        /// </summary>
        //TODO: Move to a domain service
        private void CalculatePerformance(Guid transactionId, Guid stockId, decimal? mae, decimal? mfe)
        {
            var entries = _transactionBook.GetLastCommittedChanges(stockId).ToList();
            var sell = entries.FirstOrDefault(e => e.TransactionId == transactionId) as ISellingTransactionBookEntry;
            var buys = entries.Where(e => e.TransactionId != transactionId).Cast<IBuyingTransactionBookEntry>();

            var performance = _transactionPerformanceService.GetPerformance(sell, buys, mfe, mae);
            _eventBus.Publish(new TransactionPerformanceCalculatedEvent(transactionId, typeof(TransactionAggregate),
                performance.ProfitAbsolute,
                performance.ProfitPercentage,
                performance.ProfitMade,
                performance.HoldingPeriod,
                performance.R,
                performance.ExitEfficiency,
                performance.EntryEfficiency,
                performance.MAEAbsolute,
                performance.MFEAbsolute));

            _eventBus.Publish(new StockOverallPerformanceChangedEvent(transactionId, typeof(TransactionAggregate), performance.ProfitAbsolute, stockId));
        }

        /// <summary>
        /// Calculates the performance for this transaction if its a dividend transaction
        /// </summary>
        //TODO: Move to a domain service
        private void CalculateDividendPerformance(Guid transactionId, Guid stockId)
        {
            var entries = _transactionBook.GetLastCommittedChanges(stockId).ToList();
            var dividend = entries.FirstOrDefault(e => e.TransactionId == transactionId) as IDividendTransactionBookEntry;
            var buys = entries.Where(e => e.TransactionId != transactionId).Cast<IBuyingTransactionBookEntry>();

            var performance = _transactionPerformanceService.GetPerformance(dividend, buys);
            _eventBus.Publish(new TransactionDividendCalculatedEvent(transactionId, typeof(TransactionAggregate),
                performance.ProfitAbsolute,
                performance.ProfitPercentage,
                performance.ProfitMade,
                performance.HoldingPeriod,
                performance.R));

            _eventBus.Publish(new StockOverallPerformanceChangedEvent(transactionId, typeof(TransactionAggregate), performance.ProfitAbsolute, stockId));
        }

    }
}
