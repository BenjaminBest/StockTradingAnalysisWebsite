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
        /// <param name="source">The items.</param>
        /// <param name="getChildren">The get children.</param>
        /// <returns></returns>
        public static IEnumerable<T> Flatten<T>(this IEnumerable<T> source, Func<T, IEnumerable<T>> getChildren)
        {
            var stack = new Stack<T>();
            foreach (var item in source)
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
        /// <param name="source">The items.</param>
        /// <param name="safeAction">The safe action.</param>
        public static void WhenNotNullOrEmpty<T>(this IEnumerable<T> source, Action<IEnumerable<T>> safeAction)
        {
            var itemsList = source?.ToList() ?? Enumerable.Empty<T>().ToList();

            if (!itemsList.Any())
                return;

            safeAction(itemsList);
        }

        /// <summary>
        /// Returns all elements of the given <paramref name="source"/> when they are of type <typeparamref name="TType1"/> 
        /// or <typeparamref name="TType2"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TType1">The type of the type1.</typeparam>
        /// <typeparam name="TType2">The type of the type2.</typeparam>
        /// <param name="source">The source.</param>
        /// <returns>Filtered items.</returns>
        public static IEnumerable<T> OfTypes<T, TType1, TType2>(this IEnumerable<T> source)
        {
            foreach (object obj in source)
            {
                if (obj is TType1 || obj is TType2)
                    yield return (T)obj;
            }
        }
    }
}
