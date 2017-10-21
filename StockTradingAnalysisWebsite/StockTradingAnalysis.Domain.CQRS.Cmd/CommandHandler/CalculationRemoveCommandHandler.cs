using StockTradingAnalysis.Domain.CQRS.Cmd.Commands;
using StockTradingAnalysis.Domain.CQRS.Cmd.Exceptions;
using StockTradingAnalysis.Domain.Events.Aggregates;
using StockTradingAnalysis.Interfaces.Commands;
using StockTradingAnalysis.Interfaces.DomainContext;

namespace StockTradingAnalysis.Domain.CQRS.Cmd.CommandHandler
{
    public class CalculationRemoveCommandHandler : ICommandHandler<CalculationRemoveCommand>
    {
        private readonly IAggregateRepository<CalculationAggregate> _repository;

        /// <summary>
        /// Initializes this object
        /// </summary>
        /// <param name="repository">The repository for the aggregate</param>
        public CalculationRemoveCommandHandler(IAggregateRepository<CalculationAggregate> repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Executes the specified command.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <returns></returns>
        /// <exception cref="DomainValidationException">Thrown if validation fails</exception>
        public void Execute(CalculationRemoveCommand command)
        {
            var aggregate = _repository.GetById(command.AggregateId);
            aggregate.Remove();

            _repository.Save(aggregate, command.OriginalVersion);
        }
    }
}