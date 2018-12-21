using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using StockTradingAnalysis.Domain.CQRS.Query.Queries;
using StockTradingAnalysis.Interfaces.Queries;
using StockTradingAnalysis.Web.Common.Interfaces;
using StockTradingAnalysis.Web.Models;

namespace StockTradingAnalysis.Web.Common.ItemResolvers
{
	/// <summary>
	/// Class FeedbackItemResolver defines an item resolver for feedbacks
	/// </summary>
	public class FeedbackItemResolver : ISelectItemResolver
	{
		private readonly IQueryDispatcher _queryDispatcher;

		/// <summary>
		/// Initializes this object
		/// </summary>
		public FeedbackItemResolver(IQueryDispatcher queryDispatcher)
		{
			_queryDispatcher = queryDispatcher;
		}

		/// <summary>
		/// Gets the identifier of this resolver
		/// </summary>
		public string Identifier => "Feedback";

		/// <summary>
		/// Returns a list with all items
		/// </summary>
		/// <returns>Items</returns>
		public IEnumerable<SelectListItem> GetItems()
		{
			var stocks = Mapper.Map<IEnumerable<SelectionViewModel>>(_queryDispatcher.Execute(new FeedbackAllQuery()));
			return new SelectList(stocks, "Id", "Text");
		}
	}
}