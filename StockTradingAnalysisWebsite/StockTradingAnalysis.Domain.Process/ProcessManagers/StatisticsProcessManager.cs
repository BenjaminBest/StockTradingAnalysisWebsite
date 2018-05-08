using System;
using System.Linq;
using StockTradingAnalysis.Domain.Events.Events;
using StockTradingAnalysis.Domain.Process.Data;
using StockTradingAnalysis.EventSourcing.Events;
using StockTradingAnalysis.Interfaces.Events;

namespace StockTradingAnalysis.Domain.Process.ProcessManagers
{
    /// <summary>
    /// The class StockProcessManager manages the process of a stock.
    /// </summary>
    /// <seealso cref="ProcessManagers.ProcessManagerBase{StatisticsProcessManagerData}" />
    /// <seealso cref="IProcessManager" />
    public class StatisticsProcessManager : ProcessManagerBase<StatisticsProcessManagerData>,
        IStartedByMessage<StockQuotationAddedEvent>,
        IStartedByMessage<StockQuotationChangedEvent>,
        IStartedByMessage<StockQuotationsAddedOrChangedEvent>,
        IStartedByMessage<ReplayFinishedEvent>,
        IStartedByMessage<TransactionPerformanceCalculatedEvent>,
        IStartedByMessage<TransactionDividendCalculatedEvent>,
        IStartedByMessage<TransactionUndoEvent>
    {
        /// <summary>
        /// The event bus
        /// </summary>
        private readonly IEventBus _eventBus;

        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionProcessManager" /> class.
        /// </summary>
        /// <param name="eventBus">The event bus.</param>
        public StatisticsProcessManager(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        /// <summary>
        /// Handles the given message <paramref name="message" />
        /// </summary>
        /// <param name="message">The message.</param>
        public void Handle(StockQuotationAddedEvent message)
        {
            if (Data.ReplayFinished)
                _eventBus.Publish(new StaticsticsBasicDataChangedEvent(message.Quotation.Date));
        }

        /// <summary>
        /// Handles the given message <paramref name="message" />
        /// </summary>
        /// <param name="message">The message.</param>
        public void Handle(StockQuotationChangedEvent message)
        {
            if (Data.ReplayFinished)
                _eventBus.Publish(new StaticsticsBasicDataChangedEvent(message.Quotation.Date));
        }

        /// <summary>
        /// Handles the given message <paramref name="message" />
        /// </summary>
        /// <param name="message">The message.</param>
        public void Handle(StockQuotationsAddedOrChangedEvent message)
        {
            if (Data.ReplayFinished)
                _eventBus.Publish(new StaticsticsBasicDataChangedEvent(message.Quotations.Min(q => q.Date)));
        }

        /// <summary>
        /// Handles the given message <paramref name="message" />
        /// </summary>
        /// <param name="message">The message.</param>
        public void Handle(ReplayFinishedEvent message)
        {
            _eventBus.Publish(new StaticsticsBasicDataChangedEvent(DateTime.MinValue));
            Data.ReplayFinished = true;

            //Long living process to make sure during replay statistics are only calculated once
            //StatusUpdate.MarkAsCompleted(this);
        }

        /// <summary>
        /// Handles the given message <paramref name="message" />
        /// </summary>
        /// <param name="message">The message.</param>
        public void Handle(TransactionUndoEvent message)
        {
            if (Data.ReplayFinished)
                _eventBus.Publish(new StaticsticsBasicDataChangedEvent(message.OrderDate));
        }

        /// <summary>
        /// Handles the given message <paramref name="message" />
        /// </summary>
        /// <param name="message">The message.</param>
        public void Handle(TransactionPerformanceCalculatedEvent message)
        {
            if (Data.ReplayFinished)
                _eventBus.Publish(new StaticsticsBasicDataChangedEvent(message.HoldingPeriod.EndDate));
        }

        /// <summary>
        /// Handles the given message <paramref name="message" />
        /// </summary>
        /// <param name="message">The message.</param>
        public void Handle(TransactionDividendCalculatedEvent message)
        {
            if (Data.ReplayFinished)
                _eventBus.Publish(new StaticsticsBasicDataChangedEvent(message.HoldingPeriod.EndDate));
        }
    }
}
