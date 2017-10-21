using StockTradingAnalysis.Domain.CQRS.Query.Queries;
using StockTradingAnalysis.Interfaces.Domain;
using StockTradingAnalysis.Interfaces.Queries;
using StockTradingAnalysis.Interfaces.ReadModel;
using System.Collections.Generic;
using System.Linq;

namespace StockTradingAnalysis.Domain.CQRS.Query.QueryHandler
{
    public class TransactionAllQueryHandler : IQueryHandler<TransactionAllQuery, IEnumerable<ITransaction>>
    {
        private readonly IModelReaderRepository<ITransaction> _modelReaderRepository;

        /// <summary>
        /// Initializes this object
        /// </summary>
        /// <param name="modelReaderRepository">The model repository to read from</param>
        public TransactionAllQueryHandler(IModelReaderRepository<ITransaction> modelReaderRepository)
        {
            _modelReaderRepository = modelReaderRepository;
        }

        /// <summary>
        /// Executes the specified query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        public IEnumerable<ITransaction> Execute(TransactionAllQuery query)
        {
            return query.Filter(_modelReaderRepository.GetAll()).OrderByDescending(t => t.OrderDate);
        }
    }
}