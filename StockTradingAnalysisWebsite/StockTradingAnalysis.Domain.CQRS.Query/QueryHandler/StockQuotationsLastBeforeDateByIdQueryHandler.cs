using System.Linq;
using StockTradingAnalysis.Domain.CQRS.Query.Queries;
using StockTradingAnalysis.Interfaces.Domain;
using StockTradingAnalysis.Interfaces.Queries;
using StockTradingAnalysis.Interfaces.ReadModel;

namespace StockTradingAnalysis.Domain.CQRS.Query.QueryHandler
{
    /// <summary>
    /// The query handler StockQuotationsLastBeforeDateByIdQueryHandler returns the last quotation for a given stock before a given date
    /// </summary>
    /// <seealso cref="Interfaces.Queries.IQueryHandler{StockQuotationsLastOfYearByIdQuery, IQuotation}" />
    public class StockQuotationsLastBeforeDateByIdQueryHandler : IQueryHandler<StockQuotationsLastBeforeDateByIdQuery, IQuotation>
    {
        /// <summary>
        /// The model reader repository
        /// </summary>
        private readonly IModelReaderRepository<IStock> _modelReaderRepository;

        /// <summary>
        /// Initializes this object
        /// </summary>
        /// <param name="modelReaderRepository">The model repository to read from</param>
        public StockQuotationsLastBeforeDateByIdQueryHandler(IModelReaderRepository<IStock> modelReaderRepository)
        {
            _modelReaderRepository = modelReaderRepository;
        }

        /// <summary>
        /// Executes the specified query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        public IQuotation Execute(StockQuotationsLastBeforeDateByIdQuery query)
        {
            return _modelReaderRepository.GetById(query.Id).Quotations
                ?.Where(q => q.Date <= query.Date)
                .OrderByDescending(q => q.Date)
                .FirstOrDefault();
        }
    }
}