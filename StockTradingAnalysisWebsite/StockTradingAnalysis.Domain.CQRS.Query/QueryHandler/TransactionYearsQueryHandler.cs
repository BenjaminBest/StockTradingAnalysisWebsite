using StockTradingAnalysis.Domain.CQRS.Query.Queries;
using StockTradingAnalysis.Interfaces.Domain;
using StockTradingAnalysis.Interfaces.Queries;
using StockTradingAnalysis.Interfaces.ReadModel;
using System.Collections.Generic;
using System.Linq;

namespace StockTradingAnalysis.Domain.CQRS.Query.QueryHandler
{
    public class TransactionYearsQueryHandler : IQueryHandler<TransactionYearsQuery, IEnumerable<int>>
    {
        private readonly IModelReaderRepository<ITransaction> _modelReaderRepository;

        /// <summary>
        /// Initializes this object
        /// </summary>
        /// <param name="modelReaderRepository">The model repository to read from</param>
        public TransactionYearsQueryHandler(IModelReaderRepository<ITransaction> modelReaderRepository)
        {
            _modelReaderRepository = modelReaderRepository;
        }

        /// <summary>
        /// Executes the specified query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        public IEnumerable<int> Execute(TransactionYearsQuery query)
        {
            return query.Filter(_modelReaderRepository.GetAll()).GroupBy(t => t.OrderDate.Year).OrderBy(t => t.Key).Select(t => t.Key);
        }
    }
}