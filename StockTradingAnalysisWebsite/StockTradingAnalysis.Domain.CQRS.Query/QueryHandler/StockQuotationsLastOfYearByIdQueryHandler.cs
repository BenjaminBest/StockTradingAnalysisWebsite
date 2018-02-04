using System;
using System.Linq;
using StockTradingAnalysis.Domain.CQRS.Query.Queries;
using StockTradingAnalysis.Interfaces.Domain;
using StockTradingAnalysis.Interfaces.Queries;
using StockTradingAnalysis.Interfaces.ReadModel;
using StockTradingAnalysis.Interfaces.Services.Core;

namespace StockTradingAnalysis.Domain.CQRS.Query.QueryHandler
{
    /// <summary>
    /// The query handler StockQuotationsLastOfYearByIdQueryHandler returns the last quotation of a given year for a given stock
    /// </summary>
    /// <seealso cref="Interfaces.Queries.IQueryHandler{StockQuotationsLastOfYearByIdQuery, IQuotation}" />
    public class StockQuotationsLastOfYearByIdQueryHandler : IQueryHandler<StockQuotationsLastOfYearByIdQuery, IQuotation>
    {
        /// <summary>
        /// The model reader repository
        /// </summary>
        private readonly IModelReaderRepository<IStock> _modelReaderRepository;

        /// <summary>
        /// The date calculation service
        /// </summary>
        private readonly IDateCalculationService _dateCalculationService;

        /// <summary>
        /// Initializes this object
        /// </summary>
        /// <param name="modelReaderRepository">The model repository to read from</param>
        /// <param name="dateCalculationService">The date calculation service.</param>
        public StockQuotationsLastOfYearByIdQueryHandler(
            IModelReaderRepository<IStock> modelReaderRepository,
            IDateCalculationService dateCalculationService)
        {
            _modelReaderRepository = modelReaderRepository;
            _dateCalculationService = dateCalculationService;
        }

        /// <summary>
        /// Executes the specified query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        public IQuotation Execute(StockQuotationsLastOfYearByIdQuery query)
        {
            var endDate = _dateCalculationService.GetEndDateOfYear(new DateTime(query.Year, 1, 1));

            return _modelReaderRepository.GetById(query.Id).Quotations
                ?.Where(q => q.Date <= endDate)
                .OrderByDescending(q => q.Date)
                .FirstOrDefault();
        }
    }
}