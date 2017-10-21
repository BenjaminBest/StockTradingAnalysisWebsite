using StockTradingAnalysis.Domain.CQRS.Cmd.Commands;
using StockTradingAnalysis.Domain.Events.Aggregates;
using StockTradingAnalysis.Interfaces.Commands;
using StockTradingAnalysis.Interfaces.DomainContext;

namespace StockTradingAnalysis.Domain.CQRS.Cmd.CommandHandler
{
    public class TransactionCalculateCommandHandler : ICommandHandler<TransactionCalculateCommand>
    {
        private readonly IAggregateRepository<TransactionAggregate> _repository;

        /// <summary>
        /// Initializes this object
        /// </summary>
        /// <param name="repository">The repository for the aggregate</param>
        public TransactionCalculateCommandHandler(IAggregateRepository<TransactionAggregate> repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Executes the specified command.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <returns></returns>
        public void Execute(TransactionCalculateCommand command)
        {
            var aggregate = _repository.GetById(command.AggregateId);

            //Calculate performance
            aggregate.CalculatePerformance();

            _repository.Save(aggregate, command.OriginalVersion);
        }
    }
}