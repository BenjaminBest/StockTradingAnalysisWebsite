using System;
using System.Collections.Generic;
using StockTradingAnalysis.Interfaces.Domain;

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
		IEnumerable<IQuotation> Get(Guid stockId);

		/// <summary>
		/// Gets all quotations for the given <paramref name="stockId"/>
		/// </summary>
		/// <param name="stockId">The stock.</param>
		/// <param name="since">The date for which quotations should be downloaded (including)</param>
		/// <returns></returns>
		IEnumerable<IQuotation> Get(Guid stockId,DateTime since);

		/// <summary>
		/// Determines whether this instance is online.
		/// </summary>
		/// <returns>
		///   <c>true</c> if this instance is online; otherwise, <c>false</c>.
		/// </returns>
		bool IsOnline();
	}
}