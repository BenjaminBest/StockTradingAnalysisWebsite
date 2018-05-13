using System;
using System.Collections.Generic;
using StockTradingAnalysis.Domain.CQRS.Query.Queries;
using StockTradingAnalysis.Interfaces.Domain;
using StockTradingAnalysis.Interfaces.Queries;
using StockTradingAnalysis.Services.External.Interfaces;
using StockTradingAnalysis.Web.Common.Interfaces;

namespace StockTradingAnalysis.Web.Common.Services
{
    /// <summary>
    /// The QuotationServiceClient implements methods to communicate with the stock quotation service
    /// </summary>
    /// <seealso cref="IQuotationServiceClient" />
    public class QuotationServiceClient : IQuotationServiceClient
    {
        /// <summary>
        /// The query dispatcher
        /// </summary>
        private readonly IQueryDispatcher _queryDispatcher;

        /// <summary>
        /// The stock quote external service
        /// </summary>
        private readonly IStockQuoteExternalService _stockQuoteExternalService;

        /// <summary>
        /// Initializes a new instance of the <see cref="QuotationServiceClient" /> class.
        /// </summary>
        /// <param name="queryDispatcher">The query dispatcher.</param>
        /// <param name="stockQuoteExternalService">The external stock quote service.</param>
        public QuotationServiceClient(
            IQueryDispatcher queryDispatcher,
            IStockQuoteExternalService stockQuoteExternalService)
        {
            _queryDispatcher = queryDispatcher;
            _stockQuoteExternalService = stockQuoteExternalService;
        }

        /// <summary>
        /// Gets all quotations for the given <paramref name="stockId" />
        /// </summary>
        /// <param name="stockId">The stock.</param>
        /// <returns></returns>
        public IEnumerable<IQuotation> Get(Guid stockId)
        {
            return _stockQuoteExternalService.Get(_queryDispatcher.Execute(new StockByIdQuery(stockId)).Wkn);
        }

        /// <summary>
        /// Determines whether this instance is online.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance is online; otherwise, <c>false</c>.
        /// </returns>
        public bool IsOnline()
        {
            return _stockQuoteExternalService.IsOnline();
        }
    }
}