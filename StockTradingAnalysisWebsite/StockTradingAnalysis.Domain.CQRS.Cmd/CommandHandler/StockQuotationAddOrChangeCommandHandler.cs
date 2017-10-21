using StockTradingAnalysis.Domain.CQRS.Cmd.Commands;
using StockTradingAnalysis.Domain.CQRS.Cmd.Exceptions;
using StockTradingAnalysis.Domain.Events.Aggregates;
using StockTradingAnalysis.Interfaces.Commands;
using StockTradingAnalysis.Interfaces.DomainContext;

namespace StockTradingAnalysis.Domain.CQRS.Cmd.CommandHandler
{
    public class StockQuotationAddOrChangeCommandHandler : ICommandHandler<StockQuotationAddOrChangeCommand>
    {
        private readonly IAggregateRepository<StockAggregate> _repository;

        /// <summary>
        /// Initializes this object
        /// </summary>
        /// <param name="repository">The repository for the aggregate</param>
        public StockQuotationAddOrChangeCommandHandler(IAggregateRepository<StockAggregate> repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Executes the specified command.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <returns></returns>
        /// <exception cref="DomainValidationException">Thrown if validation fails</exception>
        public void Execute(StockQuotationAddOrChangeCommand command)
        {
            if (command.Quotation == null)
                throw new DomainValidationException("command.Quotation", "Quotation has no value");

            var aggregate = _repository.GetById(command.AggregateId);

            aggregate.AddOrChangeQuotation(command.Quotation);

            _repository.Save(aggregate, command.OriginalVersion);
        }
    }
}