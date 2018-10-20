using System;
using System.Collections.Generic;
using StockTradingAnalysis.Interfaces.Domain;

namespace StockTradingAnalysis.Services.External.Common
{
	/// <summary>
	/// Class QuotationDownload is a base class which is able to download a page. The data handling logic must
	/// be implemented in the derived class.
	/// </summary>
	public abstract class QuotationDownload
	{
		/// <summary>
		/// WKN
		/// </summary>
		protected string Wkn;

		/// <summary>
		/// Url to download
		/// </summary>
		private Uri _website;

		/// <summary>
		/// Response data
		/// </summary>
		protected string Response;

		/// <summary>
		/// Initializes this class
		/// </summary>
		/// <param name="wkn">WKN</param>
		/// <param name="website">Url</param>
		/// <returns></returns>
		public QuotationDownload Initialize(string wkn, string website)
		{
			Wkn = wkn;
			_website = new Uri(website);

			return this;
		}

		/// <summary>
		/// Downloads the url and writes the result response
		/// </summary>
		/// <returns></returns>
		public QuotationDownload Download()
		{
			Response = HtmlDownload.CreateHttpClient(_website).Result;

			return this;
		}

		/// <summary>
		/// Extract the needed information out of the plain html response. This is different for multiple pages.
		/// </summary>
		/// <returns></returns>
		public abstract IEnumerable<IQuotation> ExtractInformation();

		/// <summary>
		/// Extract the needed information out of the plain html response. This is different for multiple pages.
		/// </summary>
		/// <param name="since">The minimum date for which quotes should be extracted</param>
		/// <returns></returns>
		public abstract IEnumerable<IQuotation> ExtractInformation(DateTime since);
	}
}