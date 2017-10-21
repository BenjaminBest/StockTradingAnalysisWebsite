using StockTradingAnalysis.Domain.CQRS.Cmd.Commands;
using StockTradingAnalysis.Domain.Events.Aggregates;
using StockTradingAnalysis.Interfaces.Commands;
using StockTradingAnalysis.Interfaces.DomainContext;

namespace StockTradingAnalysis.Domain.CQRS.Cmd.CommandHandler
{
    public class TransactionCalculateDividendCommandHandler : ICommandHandler<TransactionCalculateDividendCommand>
    {
        private readonly IAggregateRepository<TransactionAggregate> _repository;

        /// <summary>
        /// Initializes this object
        /// </summary>
        /// <param name="repository">The repository for the aggregate</param>
        public TransactionCalculateDividendCommandHandler(IAggregateRepository<TransactionAggregate> repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Executes the specified command.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <returns></returns>
        public void Execute(TransactionCalculateDividendCommand command)
        {
            var aggregate = _repository.GetById(command.AggregateId);

            //Calculate performance
            aggregate.CalculateDividendPerformance();

            _repository.Save(aggregate, command.OriginalVersion);
        }
    }
}