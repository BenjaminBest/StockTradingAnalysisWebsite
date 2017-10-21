using StockTradingAnalysis.Domain.CQRS.Cmd.Commands;
using StockTradingAnalysis.Domain.CQRS.Cmd.Exceptions;
using StockTradingAnalysis.Domain.Events.Aggregates;
using StockTradingAnalysis.Interfaces.Commands;
using StockTradingAnalysis.Interfaces.DomainContext;

namespace StockTradingAnalysis.Domain.CQRS.Cmd.CommandHandler
{
    public class CalculationAddCommandHandler : ICommandHandler<CalculationAddCommand>
    {
        private readonly IAggregateRepository<CalculationAggregate> _repository;

        /// <summary>
        /// Initializes this object
        /// </summary>
        /// <param name="repository">The repository for the aggregate</param>
        public CalculationAddCommandHandler(IAggregateRepository<CalculationAggregate> repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Executes the specified command.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <returns></returns>
        /// <exception cref="DomainValidationException">Thrown if validation fails</exception>
        public void Execute(CalculationAddCommand command)
        {
            //TODO: Validate if name is unique ->Exception: Resources.Validation_CalculationNameExists(felt noch)
            //TODO: Validate if name, description is not empty            

            var aggregate = new CalculationAggregate(
                command.AggregateId,
                command.Name,
                command.Wkn,
                command.Multiplier,
                command.StrikePrice,
                command.Underlying,
                command.InitialSl,
                command.InitialTp,
                command.PricePerUnit,
                command.OrderCosts,
                command.Description,
                command.Units,
                command.IsLong);

            _repository.Save(aggregate, -1);
        }
    }
}