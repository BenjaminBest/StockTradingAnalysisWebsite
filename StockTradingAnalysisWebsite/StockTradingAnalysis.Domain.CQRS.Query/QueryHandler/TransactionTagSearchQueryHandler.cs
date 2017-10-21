using System;
using System.Collections.Generic;
using System.Linq;
using StockTradingAnalysis.Domain.CQRS.Query.Queries;
using StockTradingAnalysis.Interfaces.Domain;
using StockTradingAnalysis.Interfaces.Queries;
using StockTradingAnalysis.Interfaces.ReadModel;

namespace StockTradingAnalysis.Domain.CQRS.Query.QueryHandler
{
    public class TransactionTagSearchQueryHandler : IQueryHandler<TransactionTagSearchQuery, IEnumerable<string>>
    {
        private readonly IModelReaderRepository<ITransaction> _modelReaderRepository;

        /// <summary>
        /// Initializes this object
        /// </summary>
        /// <param name="modelReaderRepository">The model repository to read from</param>
        public TransactionTagSearchQueryHandler(IModelReaderRepository<ITransaction> modelReaderRepository)
        {
            _modelReaderRepository = modelReaderRepository;
        }

        /// <summary>
        /// Executes the specified query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        public IEnumerable<string> Execute(TransactionTagSearchQuery query)
        {
            var items = _modelReaderRepository.GetAll().Select(t => t.Tag);

            if (!query.SearchTerm.Equals(" "))
                items = items.Where(i => i.StartsWith(query.SearchTerm, StringComparison.InvariantCultureIgnoreCase));

            return items.Select(i => i).Distinct();
        }
    }
}