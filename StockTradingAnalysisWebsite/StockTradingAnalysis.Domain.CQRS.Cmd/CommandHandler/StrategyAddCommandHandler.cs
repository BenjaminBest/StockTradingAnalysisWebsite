using StockTradingAnalysis.Domain.CQRS.Cmd.Commands;
using StockTradingAnalysis.Domain.CQRS.Cmd.Exceptions;
using StockTradingAnalysis.Domain.Events.Aggregates;
using StockTradingAnalysis.Interfaces.Commands;
using StockTradingAnalysis.Interfaces.DomainContext;

namespace StockTradingAnalysis.Domain.CQRS.Cmd.CommandHandler
{
    public class StrategyAddCommandHandler : ICommandHandler<StrategyAddCommand>
    {
        private readonly IAggregateRepository<StrategyAggregate> _repository;

        /// <summary>
        /// Initializes this object
        /// </summary>
        /// <param name="repository">The repository for the aggregate</param>
        public StrategyAddCommandHandler(IAggregateRepository<StrategyAggregate> repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Executes the specified command.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <returns></returns>
        /// <exception cref="DomainValidationException">Thrown if validation fails</exception>
        public void Execute(StrategyAddCommand command)
        {
            //TODO: Validate if name is unique ->Exception: Resources.Validation_StrategyNameExists(fehlt noch)
            //TODO: Validate if name, description is not empty
            //TODO: Validate if image is valid

            var aggregate = new StrategyAggregate(
                command.AggregateId,
                command.Name,
                command.Description,
                command.Image);

            _repository.Save(aggregate, -1);
        }
    }
}