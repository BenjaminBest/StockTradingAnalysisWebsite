using System.Linq;
using StockTradingAnalysis.Domain.CQRS.Query.Queries;
using StockTradingAnalysis.Interfaces.Domain;
using StockTradingAnalysis.Interfaces.Queries;
using StockTradingAnalysis.Interfaces.ReadModel;

namespace StockTradingAnalysis.Domain.CQRS.Query.QueryHandler
{
    /// <summary>
    /// The query handler StockQuotationsLastestByIdQueryHandler returns the lastest quote for a given stock.
    /// </summary>
    /// <seealso cref="Interfaces.Queries.IQueryHandler{StockQuotationsLastOfYearByIdQuery, IQuotation}" />
    public class StockQuotationsLastestByIdQueryHandler : IQueryHandler<StockQuotationsLatestByIdQuery, IQuotation>
    {
        /// <summary>
        /// The model reader repository
        /// </summary>
        private readonly IModelReaderRepository<IStock> _modelReaderRepository;

        /// <summary>
        /// Initializes this object
        /// </summary>
        /// <param name="modelReaderRepository">The model repository to read from</param>
        public StockQuotationsLastestByIdQueryHandler(IModelReaderRepository<IStock> modelReaderRepository)
        {
            _modelReaderRepository = modelReaderRepository;
        }

        /// <summary>
        /// Executes the specified query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        public IQuotation Execute(StockQuotationsLatestByIdQuery query)
        {
            return _modelReaderRepository.GetById(query.Id).Quotations
                .OrderByDescending(q => q.Date)
                .FirstOrDefault();
        }
    }
}