using System;
using System.Collections.Generic;
using StockTradingAnalysis.Domain.Events.Domain;

namespace StockTradingAnalysis.Web.Common.Interfaces
{
    /// <summary>
    /// The IQuotationServiceClient defines methods to communicate with the quotation service
    /// </summary>
    public interface IQuotationServiceClient
    {
        /// <summary>
        /// Gets all quotations for the given <paramref name="stockId"/>
        /// </summary>
        /// <param name="stockId">The stock.</param>
        /// <returns></returns>
        IEnumerable<Quotation> Get(Guid stockId);

        /// <summary>
        /// Determines whether this instance is online.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance is online; otherwise, <c>false</c>.
        /// </returns>
        bool IsOnline();
    }
}