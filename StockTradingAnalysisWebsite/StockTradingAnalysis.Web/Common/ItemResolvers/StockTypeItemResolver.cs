using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using StockTradingAnalysis.Domain.CQRS.Query.Queries;
using StockTradingAnalysis.Interfaces.Queries;
using StockTradingAnalysis.Web.Common.Interfaces;

namespace StockTradingAnalysis.Web.Common.ItemResolvers
{
    /// <summary>
    /// Class StockTypeItemResolver defines an item resolver over the different stock types
    /// </summary>
    public class StockTypeItemResolver : ISelectItemResolver
    {
        private readonly IQueryDispatcher _queryDispatcher;

        public StockTypeItemResolver(IQueryDispatcher queryDispatcher)
        {
            _queryDispatcher = queryDispatcher;
        }

        /// <summary>
        /// Gets the identifier of this resolver
        /// </summary>
        public string Identifier => "StockType";

        /// <summary>
        /// Returns a list with all items
        /// </summary>
        /// <returns>Items</returns>
        public IEnumerable<SelectListItem> GetItems()
        {
            var items = new List<SelectListItem>();

            var types = _queryDispatcher.Execute(new StockTypeSearchQuery(string.Empty)).Take(10);

            items.Add(new SelectListItem { Text = Resources.ViewTextFilterAll, Value = "" });
            items.AddRange(types.Select(stock => new SelectListItem { Text = stock, Value = stock }));

            return items;
        }
    }
}