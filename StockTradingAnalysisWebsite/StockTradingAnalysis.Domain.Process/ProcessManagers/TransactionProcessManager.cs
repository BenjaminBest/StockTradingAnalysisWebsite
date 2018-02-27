using StockTradingAnalysis.Domain.CQRS.Cmd.Commands;
using StockTradingAnalysis.Domain.CQRS.Query.Queries;
using StockTradingAnalysis.Domain.Process.Data;
using StockTradingAnalysis.Interfaces.Commands;
using StockTradingAnalysis.Interfaces.Events;
using StockTradingAnalysis.Interfaces.Queries;

namespace StockTradingAnalysis.Domain.Process.ProcessManagers
{
    /// <summary>
    /// The class TransactionProcessManager manages the process of a transaction.
    /// </summary>
    /// <seealso cref="ProcessManagerBase{TData}" />
    /// <seealso cref="Interfaces.Events.IStartedByMessage{TransactionDividendCommand}" />
    /// <seealso cref="Interfaces.Events.IStartedByMessage{TransactionSellCommand}" />
    /// <seealso cref="IProcessManager" />
    public class TransactionProcessManager : ProcessManagerBase<TransactionProcessManagerData>,
        IStartedByMessage<TransactionDividendCommand>,
        IStartedByMessage<TransactionSellCommand>
    {
        /// <summary>
        /// The command dispatcher
        /// </summary>
        private readonly ICommandDispatcher _commandDispatcher;

        /// <summary>
        /// The query dispatcher
        /// </summary>
        private readonly IQueryDispatcher _queryDispatcher;

        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionProcessManager"/> class.
        /// </summary>
        /// <param name="commandDispatcher">The command dispatcher.</param>
        /// <param name="queryDispatcher">The query dispatcher.</param>
        public TransactionProcessManager(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
        {
            _commandDispatcher = commandDispatcher;
            _queryDispatcher = queryDispatcher;
        }

        /// <summary>
        /// Handles the given command <paramref name="command"/>
        /// </summary>
        /// <param name="command">The command</param>
        public void Handle(TransactionDividendCommand command)
        {
            _commandDispatcher.Execute(new TransactionCalculateDividendCommand(command.AggregateId,
                _queryDispatcher.Execute(new TransactionByIdQuery(command.AggregateId)).OriginalVersion));

            StatusUpdate.MarkAsCompleted(this);
        }

        /// <summary>
        /// Handles the given command <paramref name="command"/>
        /// </summary>
        /// <param name="command">The command</param>
        public void Handle(TransactionSellCommand command)
        {
            _commandDispatcher.Execute(new TransactionCalculateCommand(command.AggregateId,
                _queryDispatcher.Execute(new TransactionByIdQuery(command.AggregateId)).OriginalVersion));

            StatusUpdate.MarkAsCompleted(this);
        }
    }
}
