using System;
using System.Collections.Generic;
using System.Linq;

namespace StockTradingAnalysis.Core.Extensions
{
    /// <summary>
    /// The class EnumerableExtensions contains extension methods for IEnumerable
    /// </summary>
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Flattens the specified get children.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items">The items.</param>
        /// <param name="getChildren">The get children.</param>
        /// <returns></returns>
        public static IEnumerable<T> Flatten<T>(this IEnumerable<T> items, Func<T, IEnumerable<T>> getChildren)
        {
            var stack = new Stack<T>();
            foreach (var item in items)
                stack.Push(item);

            while (stack.Count > 0)
            {
                var current = stack.Pop();
                yield return current;

                var children = getChildren(current);
                if (children == null) continue;

                foreach (var child in children)
                    stack.Push(child);
            }
        }

        /// <summary>
        /// Excecutes the <paramref name="safeAction"/> when the collection of items is not null or empty.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items">The items.</param>
        /// <param name="safeAction">The safe action.</param>
        public static void WhenNotNullOrEmpty<T>(this IEnumerable<T> items, Action<IEnumerable<T>> safeAction)
        {
            var itemsList = items?.ToList() ?? Enumerable.Empty<T>().ToList();

            if (!itemsList.Any())
                return;

            safeAction(itemsList);
        }
    }
}
