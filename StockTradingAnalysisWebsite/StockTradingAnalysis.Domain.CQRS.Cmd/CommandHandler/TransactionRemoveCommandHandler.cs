using StockTradingAnalysis.Domain.CQRS.Cmd.Commands;
using StockTradingAnalysis.Domain.Events.Aggregates;
using StockTradingAnalysis.Interfaces.Commands;
using StockTradingAnalysis.Interfaces.DomainContext;

namespace StockTradingAnalysis.Domain.CQRS.Cmd.CommandHandler
{
    /// <summary>
    /// The TransactionRemoveCommandHandler removes a transaction
    /// </summary>
    /// <seealso cref="Interfaces.Commands.ICommandHandler{TransactionRemoveCommand}" />
    public class TransactionRemoveCommandHandler : ICommandHandler<TransactionRemoveCommand>
    {
        private readonly IAggregateRepository<TransactionAggregate> _repository;

        /// <summary>
        /// Initializes this object
        /// </summary>
        /// <param name="repository">The repository for the aggregate</param>
        public TransactionRemoveCommandHandler(IAggregateRepository<TransactionAggregate> repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Executes the specified command.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <returns></returns>
        public void Execute(TransactionRemoveCommand command)
        {
            //TODO: Validation

            var aggregate = _repository.GetById(command.AggregateId);
            aggregate.Undo();

            _repository.Save(aggregate, command.OriginalVersion);
        }
    }
}