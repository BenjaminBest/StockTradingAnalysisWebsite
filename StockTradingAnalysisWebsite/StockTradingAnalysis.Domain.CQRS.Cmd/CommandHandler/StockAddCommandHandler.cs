using StockTradingAnalysis.Domain.CQRS.Cmd.Commands;
using StockTradingAnalysis.Domain.Events.Aggregates;
using StockTradingAnalysis.Interfaces.Commands;
using StockTradingAnalysis.Interfaces.Domain;
using StockTradingAnalysis.Interfaces.DomainContext;
using System.Collections.Generic;

namespace StockTradingAnalysis.Domain.CQRS.Cmd.CommandHandler
{
    public class StockAddCommandHandler : ICommandHandler<StockAddCommand>
    {
        private readonly IAggregateRepository<StockAggregate> _repository;

        /// <summary>
        /// Initializes this object
        /// </summary>
        /// <param name="repository">The repository for the aggregate</param>
        public StockAddCommandHandler(IAggregateRepository<StockAggregate> repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Executes the specified command.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <returns></returns>
        /// <exception cref="DomainValidationException">Thrown if validation fails</exception>
        public void Execute(StockAddCommand command)
        {
            //TODO: Validate if name of stock and wkn is unique bei gleichem type ->Exception: Resources.Validation_StockNameExists
            //TODO: Validate if name, wkn and type is not empty

            var aggregate = new StockAggregate(
                command.AggregateId,
                command.Name,
                command.Wkn,
                command.Type,
                command.LongShort,
                new List<IQuotation>());

            _repository.Save(aggregate, -1);
        }
    }
}