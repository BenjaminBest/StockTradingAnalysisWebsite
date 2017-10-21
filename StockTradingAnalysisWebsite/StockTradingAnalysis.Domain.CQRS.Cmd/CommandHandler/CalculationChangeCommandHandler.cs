using StockTradingAnalysis.Domain.CQRS.Cmd.Commands;
using StockTradingAnalysis.Domain.CQRS.Cmd.Exceptions;
using StockTradingAnalysis.Domain.Events.Aggregates;
using StockTradingAnalysis.Interfaces.Commands;
using StockTradingAnalysis.Interfaces.DomainContext;

namespace StockTradingAnalysis.Domain.CQRS.Cmd.CommandHandler
{
    public class CalculationChangeCommandHandler : ICommandHandler<CalculationChangeCommand>
    {
        private readonly IAggregateRepository<CalculationAggregate> _repository;

        /// <summary>
        /// Initializes this object
        /// </summary>
        /// <param name="repository">The repository for the aggregate</param>
        public CalculationChangeCommandHandler(IAggregateRepository<CalculationAggregate> repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Executes the specified command.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <returns></returns>
        /// <exception cref="DomainValidationException">Thrown if validation fails</exception>
        public void Execute(CalculationChangeCommand command)
        {
            var aggregate = _repository.GetById(command.AggregateId);

            aggregate.ChangeName(command.Name);
            aggregate.ChangeWkn(command.Wkn);
            aggregate.ChangeMultiplier(command.Multiplier);
            aggregate.ChangeStrikePrice(command.StrikePrice);
            aggregate.ChangeUnderlying(command.Underlying);
            aggregate.ChangeInitialSl(command.InitialSl);
            aggregate.ChangeInitialTp(command.InitialTp);
            aggregate.ChangePricePerUnit(command.PricePerUnit);
            aggregate.ChangeOrderCosts(command.OrderCosts);
            aggregate.ChangeDescription(command.Description);
            aggregate.ChangeUnits(command.Units);
            aggregate.ChangeIsLong(command.IsLong);

            _repository.Save(aggregate, command.OriginalVersion);
        }
    }
}