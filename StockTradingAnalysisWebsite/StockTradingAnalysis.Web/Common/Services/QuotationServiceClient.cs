using System;
using System.Collections.Generic;
using System.Linq;
using StockTradingAnalysis.Core.Common;
using StockTradingAnalysis.Domain.CQRS.Query.Queries;
using StockTradingAnalysis.Domain.Events.Domain;
using StockTradingAnalysis.Interfaces.Configuration;
using StockTradingAnalysis.Interfaces.Queries;
using StockTradingAnalysis.Interfaces.Services;
using StockTradingAnalysis.Web.Common.Configuration;
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
        /// The configuration registry
        /// </summary>
        private readonly IConfigurationRegistry _configurationRegistry;

        /// <summary>
        /// The query dispatcher
        /// </summary>
        private readonly IQueryDispatcher _queryDispatcher;

        /// <summary>
        /// The serializer service
        /// </summary>
        private readonly ISerializerService _serializerService;

        /// <summary>
        /// Initializes a new instance of the <see cref="QuotationServiceClient" /> class.
        /// </summary>
        /// <param name="configurationRegistry">The configuration registry.</param>
        /// <param name="queryDispatcher">The query dispatcher.</param>
        /// <param name="serializerService">The serializer service.</param>
        public QuotationServiceClient(IConfigurationRegistry configurationRegistry, IQueryDispatcher queryDispatcher, ISerializerService serializerService)
        {
            _configurationRegistry = configurationRegistry;
            _queryDispatcher = queryDispatcher;
            _serializerService = serializerService;
        }

        /// <summary>
        /// Gets all quotations for the given <paramref name="stockId" />
        /// </summary>
        /// <param name="stockId">The stock.</param>
        /// <returns></returns>
        public IEnumerable<Quotation> Get(Guid stockId)
        {
            var stock = _queryDispatcher.Execute(new StockByIdQuery(stockId));

            var result = HtmlDownload.CreateHttpClientSync(new Uri(_configurationRegistry.GetValue<string>(ConfigurationKeys.StockQuoteServiceBaseUrl) + $"/{stock.Wkn}"));

            if (string.IsNullOrEmpty(result))
                return Enumerable.Empty<Quotation>();

            return _serializerService.Deserialize<IEnumerable<Quotation>>(result).ToList();
        }

        /// <summary>
        /// Determines whether this instance is online.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance is online; otherwise, <c>false</c>.
        /// </returns>
        public bool IsOnline()
        {
            var result = HtmlDownload.CreateHttpClientSync(new Uri(_configurationRegistry.GetValue<string>(ConfigurationKeys.StockQuoteOnlineCheckUrl)));

            return _serializerService.Deserialize<bool>(result);
        }
    }
}