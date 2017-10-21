using System.Collections.Generic;
using System.Linq;

namespace StockTradingAnalysis.Core.Extensions
{
    /// <summary>
    /// The class HashSetExtensions contains extention methods for a hashset
    /// </summary>
    public static class HashSetExtensions
    {
        /// <summary>
        /// Adds a list of items to a hashset
        /// </summary>
        /// <typeparam name="T">The type of the items</typeparam>
        /// <param name="hashset">Hashset</param>
        /// <param name="items">List of items</param>
        /// <returns></returns>
        public static bool AddRange<T>(this HashSet<T> hashset, IEnumerable<T> items)
        {
            return items.Aggregate(true, (current, item) => current & hashset.Add(item));
        }
    }
}
