using StockTradingAnalysis.Domain.CQRS.Cmd.Commands;
using StockTradingAnalysis.Domain.CQRS.Cmd.Exceptions;
using StockTradingAnalysis.Domain.Events.Aggregates;
using StockTradingAnalysis.Interfaces.Commands;
using StockTradingAnalysis.Interfaces.Domain;
using StockTradingAnalysis.Interfaces.DomainContext;

namespace StockTradingAnalysis.Domain.CQRS.Cmd.CommandHandler
{
    public class TransactionDividendCommandHandler : ICommandHandler<TransactionDividendCommand>
    {
        private readonly IAggregateRepository<TransactionAggregate> _repository;
        private readonly ITransactionBook _book;

        /// <summary>
        /// Initializes this object
        /// </summary>
        /// <param name="repository">The repository for the aggregate</param>
        /// <param name="book">The transation book</param>
        public TransactionDividendCommandHandler(IAggregateRepository<TransactionAggregate> repository, ITransactionBook book)
        {
            _repository = repository;
            _book = book;
        }

        /// <summary>
        /// Executes the specified command.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <returns></returns>
        public void Execute(TransactionDividendCommand command)
        {
            if (command.Shares == 0)
                throw new DomainValidationException("Shares", "Cannot sell zero units");

            //TODO: Further Validation
            if (_book.GetOpenPosition(command.StockId).Shares < command.Shares)
                throw new DomainValidationException("Shares", "The amount of available units for the stock is smaller than those used in the dividend.");

            //Create new transaction
            var transaction = new TransactionAggregate();

            transaction.CreateDividendTransaction(
                command.AggregateId,
                command.OrderDate,
                command.Shares,
                command.PricePerShare,
                command.OrderCosts,
                command.Description,
                command.Tag,
                command.Image,
                command.StockId,
                command.Taxes);

            //Save transaction
            _repository.Save(transaction, -1);
        }
    }
}