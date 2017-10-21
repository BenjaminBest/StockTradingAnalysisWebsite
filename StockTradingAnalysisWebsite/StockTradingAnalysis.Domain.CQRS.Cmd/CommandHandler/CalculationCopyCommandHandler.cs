using StockTradingAnalysis.Domain.CQRS.Cmd.Commands;
using StockTradingAnalysis.Domain.CQRS.Cmd.Exceptions;
using StockTradingAnalysis.Domain.Events.Aggregates;
using StockTradingAnalysis.Interfaces.Commands;
using StockTradingAnalysis.Interfaces.DomainContext;

namespace StockTradingAnalysis.Domain.CQRS.Cmd.CommandHandler
{
    public class CalculationCopyCommandHandler : ICommandHandler<CalculationCopyCommand>
    {
        private readonly IAggregateRepository<CalculationAggregate> _repository;

        /// <summary>
        /// Initializes this object
        /// </summary>
        /// <param name="repository">The repository for the aggregate</param>
        public CalculationCopyCommandHandler(IAggregateRepository<CalculationAggregate> repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Executes the specified command.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <returns></returns>
        /// <exception cref="DomainValidationException">Thrown if validation fails</exception>
        public void Execute(CalculationCopyCommand command)
        {
            var aggregate = _repository.GetById(command.OriginalId);

            var copy = new CalculationAggregate();
            copy.Copy(aggregate, command.AggregateId);

            _repository.Save(copy, -1);
        }
    }
}