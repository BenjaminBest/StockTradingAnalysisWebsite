using StockTradingAnalysis.Domain.CQRS.Query.Queries;
using StockTradingAnalysis.Interfaces.Domain;
using StockTradingAnalysis.Interfaces.Queries;
using StockTradingAnalysis.Interfaces.ReadModel;

namespace StockTradingAnalysis.Domain.CQRS.Query.QueryHandler
{
    public class CalculationByIdQueryHandler : IQueryHandler<CalculationByIdQuery, ICalculation>
    {
        private readonly IModelReaderRepository<ICalculation> _modelReaderRepository;

        /// <summary>
        /// Initializes this object
        /// </summary>
        /// <param name="modelReaderRepository">The model repository to read from</param>
        public CalculationByIdQueryHandler(IModelReaderRepository<ICalculation> modelReaderRepository)
        {
            _modelReaderRepository = modelReaderRepository;
        }

        /// <summary>
        /// Executes the specified query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        public ICalculation Execute(CalculationByIdQuery query)
        {
            return _modelReaderRepository.GetById(query.Id);
        }
    }
}