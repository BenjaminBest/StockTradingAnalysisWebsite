using System;
using System.Collections.Generic;
using System.Linq;
using StockTradingAnalysis.Core.Common;
using StockTradingAnalysis.Interfaces.Common;
using StockTradingAnalysis.Interfaces.Domain;
using StockTradingAnalysis.Services.External.Interfaces;
using StockTradingAnalysis.Services.External.Providers;

namespace StockTradingAnalysis.Services.External.Services
{
	/// <summary>
	/// The StockQuoteExternalService connects to an external resource and dowloads quotes.
	/// </summary>
	/// <seealso cref="IStockQuoteExternalService" />
	public class StockQuoteExternalService : IStockQuoteExternalService
	{
		/// <summary>
		/// The logging service
		/// </summary>
		private readonly ILoggingService _loggingService;

		/// <summary>
		/// Initializes a new instance of the <see cref="StockQuoteExternalService" /> class.
		/// </summary>
		/// <param name="loggingService">The logging service.</param>
		public StockQuoteExternalService(ILoggingService loggingService)
		{
			_loggingService = loggingService;
		}

		/// <summary>
		/// Gets the quotes for the specified WKN.
		/// </summary>
		/// <param name="wkn">The WKN.</param>
		/// <returns></returns>
		public IEnumerable<IQuotation> Get(string wkn)
		{
			return Get(wkn, DateTime.MinValue);
		}

		/// <summary>
		/// Gets the quotes for the specified WKN.
		/// </summary>
		/// <param name="wkn">The WKN.</param>
		/// <param name="since">The minimum date for getting quotes (including).</param>
		/// <returns></returns>
		public IEnumerable<IQuotation> Get(string wkn, DateTime since)
		{
			var result = Enumerable.Empty<IQuotation>();

			try
			{
				long msec = 0;
				using (new TimeMeasure(t => msec = t))
				{
					var provider = new QuotationDownloadBoerseDuesseldorf();

					result = provider
						.Initialize(wkn, $"http://www.boerse-duesseldorf.de/aktien/wkn/{wkn}/historische_kurse")
						.Download()
						.ExtractInformation(since);
				}

				_loggingService.Debug($"Downloaded quotes for {wkn} in {msec / 1000} seconds");
			}
			catch (Exception ex)
			{
				_loggingService.Error($"Download of quotes for {wkn} failed with message '{ex.Message}'");
			}

			return result;
		}

		/// <summary>
		/// Determines whether this instance is online.
		/// </summary>
		/// <returns>
		///   <c>true</c> if this instance is online; otherwise, <c>false</c>.
		/// </returns>
		public bool IsOnline()
		{
			//TODO: Implement something useful or remove
			return true;
		}
	}
}
