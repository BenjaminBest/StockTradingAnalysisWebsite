using StockTradingAnalysis.Domain.CQRS.Query.Queries;
using StockTradingAnalysis.Interfaces.Domain;
using StockTradingAnalysis.Interfaces.Queries;
using StockTradingAnalysis.Interfaces.ReadModel;
using System.Collections.Generic;
using System.Linq;

namespace StockTradingAnalysis.Domain.CQRS.Query.QueryHandler
{
    public class TransactionTagAllQueryHandler : IQueryHandler<TransactionTagAllQuery, IEnumerable<string>>
    {
        private readonly IModelReaderRepository<ITransaction> _modelReaderRepository;

        /// <summary>
        /// Initializes this object
        /// </summary>
        /// <param name="modelReaderRepository">The model repository to read from</param>
        public TransactionTagAllQueryHandler(IModelReaderRepository<ITransaction> modelReaderRepository)
        {
            _modelReaderRepository = modelReaderRepository;
        }

        /// <summary>
        /// Executes the specified query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        public IEnumerable<string> Execute(TransactionTagAllQuery query)
        {
            return _modelReaderRepository.GetAll().GroupBy(t => t.Tag).OrderBy(t => t.Key).Select(t => t.Key).Where(t => !string.IsNullOrEmpty(t));
        }
    }
}