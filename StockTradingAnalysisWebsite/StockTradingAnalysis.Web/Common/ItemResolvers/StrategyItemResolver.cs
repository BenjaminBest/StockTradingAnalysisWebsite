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
    /// Class StrategyItemResolver defines an item resolver for strategies
    /// </summary>
    public class StrategyItemResolver : ISelectItemResolver
    {
        private readonly IQueryDispatcher _queryDispatcher;

        /// <summary>
        /// Initializes this object
        /// </summary>
        public StrategyItemResolver(IQueryDispatcher queryDispatcher)
        {
            _queryDispatcher = queryDispatcher;
        }

        /// <summary>
        /// Gets the identifier of this resolver
        /// </summary>
        public string Identifier => "Strategy";

        /// <summary>
        /// Returns a list with all items
        /// </summary>
        /// <returns>Items</returns>
        public IEnumerable<SelectListItem> GetItems()
        {
            var stocks = Mapper.Map<IEnumerable<SelectionViewModel>>(_queryDispatcher.Execute(new StrategyAllQuery()));
            return new SelectList(stocks, "Id", "Text");
        }
    }
}