using StockTradingAnalysis.Domain.CQRS.Cmd.Commands;
using StockTradingAnalysis.Domain.CQRS.Cmd.Exceptions;
using StockTradingAnalysis.Domain.Events.Aggregates;
using StockTradingAnalysis.Interfaces.Commands;
using StockTradingAnalysis.Interfaces.DomainContext;

namespace StockTradingAnalysis.Domain.CQRS.Cmd.CommandHandler
{
    public class StrategyChangeCommandHandler : ICommandHandler<StrategyChangeCommand>
    {
        private readonly IAggregateRepository<StrategyAggregate> _repository;

        /// <summary>
        /// Initializes this object
        /// </summary>
        /// <param name="repository">The repository for the aggregate</param>
        public StrategyChangeCommandHandler(IAggregateRepository<StrategyAggregate> repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Executes the specified command.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <returns></returns>
        /// <exception cref="DomainValidationException">Thrown if validation fails</exception>
        public void Execute(StrategyChangeCommand command)
        {
            var aggregate = _repository.GetById(command.AggregateId);

            aggregate.ChangeName(command.Name);
            aggregate.ChangeDescription(command.Description);
            aggregate.ChangeImage(command.Image);

            _repository.Save(aggregate, command.OriginalVersion);
        }
    }
}