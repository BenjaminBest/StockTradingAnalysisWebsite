using StockTradingAnalysis.Domain.CQRS.Cmd.Commands;
using StockTradingAnalysis.Domain.Events.Aggregates;
using StockTradingAnalysis.Interfaces.Commands;
using StockTradingAnalysis.Interfaces.DomainContext;

namespace StockTradingAnalysis.Domain.CQRS.Cmd.CommandHandler
{
    public class TransactionBuyCommandHandler : ICommandHandler<TransactionBuyCommand>
    {
        private readonly IAggregateRepository<TransactionAggregate> _repository;

        /// <summary>
        /// Initializes this object
        /// </summary>
        /// <param name="repository">The repository for the aggregate</param>
        public TransactionBuyCommandHandler(
            IAggregateRepository<TransactionAggregate> repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Executes the specified command.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <returns></returns>
        public void Execute(TransactionBuyCommand command)
        {
            //TODO: Validation            

            //Create new transaction
            var transaction = new TransactionAggregate();

            transaction.CreateBuyingTransaction(
                command.AggregateId,
                command.OrderDate,
                command.Units,
                command.PricePerUnit,
                command.OrderCosts,
                command.Description,
                command.Tag,
                command.Image,
                command.InitialSL,
                command.InitialTP,
                command.StockId,
                command.StrategyId);

            //Save transaction
            _repository.Save(transaction, -1);
        }
    }
}