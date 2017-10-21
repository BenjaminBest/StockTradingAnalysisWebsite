using StockTradingAnalysis.Domain.CQRS.Cmd.Commands;
using StockTradingAnalysis.Domain.Events.Aggregates;
using StockTradingAnalysis.Interfaces.Commands;
using StockTradingAnalysis.Interfaces.DomainContext;

namespace StockTradingAnalysis.Domain.CQRS.Cmd.CommandHandler
{
    public class StockChangeCommandHandler : ICommandHandler<StockChangeCommand>
    {
        private readonly IAggregateRepository<StockAggregate> _repository;

        /// <summary>
        /// Initializes this object
        /// </summary>
        /// <param name="repository">The repository for the aggregate</param>
        public StockChangeCommandHandler(IAggregateRepository<StockAggregate> repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Executes the specified command.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <returns></returns>
        /// <exception cref="DomainValidationException">Thrown if validation fails</exception>
        public void Execute(StockChangeCommand command)
        {
            var aggregate = _repository.GetById(command.AggregateId);

            aggregate.ChangeName(command.Name);
            aggregate.ChangeLongShort(command.LongShort);
            aggregate.ChangeType(command.Type);
            aggregate.ChangeWkn(command.Wkn);

            _repository.Save(aggregate, command.OriginalVersion);
        }
    }
}