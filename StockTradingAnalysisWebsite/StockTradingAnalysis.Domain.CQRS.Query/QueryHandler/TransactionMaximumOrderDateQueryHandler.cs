using System;
using System.Linq;
using StockTradingAnalysis.Domain.CQRS.Query.Queries;
using StockTradingAnalysis.Interfaces.Domain;
using StockTradingAnalysis.Interfaces.Queries;
using StockTradingAnalysis.Interfaces.ReadModel;

namespace StockTradingAnalysis.Domain.CQRS.Query.QueryHandler
{
    /// <summary>
    /// The TransactionMaximumOrderDateQueryHandler returns the newest order date of the transactions after the filters were applied.
    /// </summary>
    /// <seealso cref="Interfaces.Queries.IQueryHandler{TransactionMinimumOrderDateQuery, DateTime}" />
    public class TransactionMaximumOrderDateQueryHandler : IQueryHandler<TransactionMaximumOrderDateQuery, DateTime>
    {
        private readonly IModelReaderRepository<ITransaction> _modelReaderRepository;

        /// <summary>
        /// Initializes this object
        /// </summary>
        /// <param name="modelReaderRepository">The model repository to read from</param>
        public TransactionMaximumOrderDateQueryHandler(IModelReaderRepository<ITransaction> modelReaderRepository)
        {
            _modelReaderRepository = modelReaderRepository;
        }

        /// <summary>
        /// Executes the specified query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        public DateTime Execute(TransactionMaximumOrderDateQuery query)
        {
            var transactions = query.Filter(_modelReaderRepository.GetAll()).ToList();

            return !transactions.Any() ? DateTime.MaxValue : transactions.Max(t => t.OrderDate);
        }
    }
}