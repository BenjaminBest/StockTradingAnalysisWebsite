using StockTradingAnalysis.Domain.CQRS.Cmd.Commands;
using StockTradingAnalysis.Domain.CQRS.Cmd.Exceptions;
using StockTradingAnalysis.Domain.Events.Aggregates;
using StockTradingAnalysis.Interfaces.Commands;
using StockTradingAnalysis.Interfaces.DomainContext;

namespace StockTradingAnalysis.Domain.CQRS.Cmd.CommandHandler
{
    public class FeedbackAddCommandHandler : ICommandHandler<FeedbackAddCommand>
    {
        private readonly IAggregateRepository<FeedbackAggregate> _repository;

        /// <summary>
        /// Initializes this object
        /// </summary>
        /// <param name="repository">The repository for the aggregate</param>
        public FeedbackAddCommandHandler(IAggregateRepository<FeedbackAggregate> repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Executes the specified command.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <returns></returns>
        /// <exception cref="DomainValidationException">Thrown if validation fails</exception>
        public void Execute(FeedbackAddCommand command)
        {
            //TODO: Validate if name is unique ->Exception: Resources.Validation_FeedbackNameExists(fehlt noch)
            //TODO: Validate if name, description is not empty

            var aggregate = new FeedbackAggregate(
                command.AggregateId,
                command.Name,
                command.Description);

            _repository.Save(aggregate, -1);
        }
    }
}