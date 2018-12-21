using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using StockTradingAnalysis.Domain.CQRS.Query.Queries;
using StockTradingAnalysis.Interfaces.Queries;
using StockTradingAnalysis.Web.Common.Interfaces;
using StockTradingAnalysis.Web.Models;

namespace StockTradingAnalysis.Web.Common.ItemResolvers
{
    /// <summary>
    /// Class StockItemResolver defines an item resolver for stocks
    /// </summary>
    public class StockItemResolver : ISelectItemResolver
    {
        private readonly IQueryDispatcher _queryDispatcher;

        /// <summary>
        /// Initializes this object
        /// </summary>
        public StockItemResolver(IQueryDispatcher queryDispatcher)
        {
            _queryDispatcher = queryDispatcher;
        }

        /// <summary>
        /// Gets the identifier of this resolver
        /// </summary>
        public string Identifier => "Stock";

        /// <summary>
        /// Returns a list with all items
        /// </summary>
        /// <returns>Items</returns>
        public IEnumerable<SelectListItem> GetItems()
        {
            var stocks = Mapper.Map<IEnumerable<SelectionViewModel>>(_queryDispatcher.Execute(new StockAllQuery()));
            return new SelectList(stocks, "Id", "Text");
        }
    }
}