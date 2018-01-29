using System;
using System.Collections.Generic;
using System.Linq;
using StockTradingAnalysis.Domain.CQRS.Query.Queries;
using StockTradingAnalysis.Interfaces.Domain;
using StockTradingAnalysis.Interfaces.Queries;
using StockTradingAnalysis.Interfaces.ReadModel;

namespace StockTradingAnalysis.Domain.CQRS.Query.QueryHandler
{
    public class StockTypeInUseSearchQueryHandler : IQueryHandler<StockTypeInUseSearchQuery, IEnumerable<string>>
    {
        /// <summary>
        /// The model reader repository
        /// </summary>
        private readonly IModelReaderRepository<IStock> _modelReaderRepository;

        /// <summary>
        /// The model transaction reader repository
        /// </summary>
        private readonly IModelReaderRepository<ITransaction> _modelTransactionReaderRepository;

        /// <summary>
        /// Initializes this object
        /// </summary>
        /// <param name="modelReaderRepository">The model repository to read from</param>
        public StockTypeInUseSearchQueryHandler(IModelReaderRepository<IStock> modelReaderRepository, IModelReaderRepository<ITransaction> modelTransactionReaderRepository)
        {
            _modelReaderRepository = modelReaderRepository;
            _modelTransactionReaderRepository = modelTransactionReaderRepository;
        }

        /// <summary>
        /// Executes the specified query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        public IEnumerable<string> Execute(StockTypeInUseSearchQuery query)
        {
            var transaction = _modelTransactionReaderRepository.GetAll();

            var items = _modelReaderRepository.GetAll()
                .Join(transaction, t1 => t1.Id, t2 => t2.Stock.Id, (t1, t2) => t1).Select(t => t.Type);

            if (!query.SearchTerm.Equals(" "))
                items = items.Where(i => i.StartsWith(query.SearchTerm, StringComparison.InvariantCultureIgnoreCase));

            return items.Select(i => i).Distinct();
        }
    }
}