using System;
using System.Linq;
using StockTradingAnalysis.Domain.CQRS.Query.Queries;
using StockTradingAnalysis.Interfaces.Domain;
using StockTradingAnalysis.Interfaces.Queries;
using StockTradingAnalysis.Interfaces.ReadModel;

namespace StockTradingAnalysis.Domain.CQRS.Query.QueryHandler
{
    /// <summary>
    /// The TransactionMinumumOrderDateQueryHandler returns the oldest order date of the transactions after the filters were applied.
    /// </summary>
    /// <seealso cref="Interfaces.Queries.IQueryHandler{TransactionMinimumOrderDateQuery, DateTime}" />
    public class TransactionMinumumOrderDateQueryHandler : IQueryHandler<TransactionMinimumOrderDateQuery, DateTime>
    {
        private readonly IModelReaderRepository<ITransaction> _modelReaderRepository;

        /// <summary>
        /// Initializes this object
        /// </summary>
        /// <param name="modelReaderRepository">The model repository to read from</param>
        public TransactionMinumumOrderDateQueryHandler(IModelReaderRepository<ITransaction> modelReaderRepository)
        {
            _modelReaderRepository = modelReaderRepository;
        }

        /// <summary>
        /// Executes the specified query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        public DateTime Execute(TransactionMinimumOrderDateQuery query)
        {
            var transactions = query.Filter(_modelReaderRepository.GetAll()).ToList();

            return !transactions.Any() ? DateTime.MinValue : transactions.Min(t => t.OrderDate);
        }
    }
}