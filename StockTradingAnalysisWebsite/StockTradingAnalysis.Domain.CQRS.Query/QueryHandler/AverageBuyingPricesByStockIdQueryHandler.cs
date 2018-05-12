using System.Collections.Generic;
using System.Linq;
using StockTradingAnalysis.Domain.CQRS.Query.Queries;
using StockTradingAnalysis.Interfaces.Domain;
using StockTradingAnalysis.Interfaces.Queries;
using StockTradingAnalysis.Interfaces.ReadModel;
using StockTradingAnalysis.Interfaces.Services.Domain;

namespace StockTradingAnalysis.Domain.CQRS.Query.QueryHandler
{
    /// <summary>
    /// The query handler AverageBuyingPricesByStockIdQueryHandler returns the average buying prices over time for the given stock.
    /// </summary>
    /// <seealso cref="Interfaces.Queries.IQueryHandler{AverageBuyingPricesByStockIdQuery, IAverageBuyingPrice}" />
    public class AverageBuyingPricesByStockIdQueryHandler : IQueryHandler<AverageBuyingPricesByStockIdQuery, IEnumerable<IAverageBuyingPrice>>
    {
        /// <summary>
        /// The model reader repository
        /// </summary>
        private readonly IModelReaderRepository<ITransaction> _modelReaderRepository;

        /// <summary>
        /// The transaction calculation service
        /// </summary>
        private readonly ITransactionCalculationService _transactionCalculationService;

        /// <summary>
        /// Initializes this object
        /// </summary>
        /// <param name="modelReaderRepository">The model repository to read from</param>
        /// <param name="transactionCalculationService">The transaction calculation service.</param>
        public AverageBuyingPricesByStockIdQueryHandler(
            IModelReaderRepository<ITransaction> modelReaderRepository,
            ITransactionCalculationService transactionCalculationService)
        {
            _modelReaderRepository = modelReaderRepository;
            _transactionCalculationService = transactionCalculationService;
        }

        /// <summary>
        /// Executes the specified query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        public IEnumerable<IAverageBuyingPrice> Execute(AverageBuyingPricesByStockIdQuery query)
        {
            var transactions = _modelReaderRepository
                .GetAll()
                .Where(t => t.Stock.Id.Equals(query.Id))
                .OrderBy(t => t.OrderDate);

            return _transactionCalculationService.CalculateAverageBuyingPrices(transactions);
        }
    }
}