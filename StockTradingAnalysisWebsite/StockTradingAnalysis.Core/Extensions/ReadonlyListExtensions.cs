using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace StockTradingAnalysis.Core.Extensions
{
    /// <summary>
    /// The class ReadonlyListExtensions contains extension methods for ReadonlyList.
    /// </summary>
    public static class ReadOnlyListExtensions
    {
        /// <summary>
        /// Returns all elements of the given <paramref name="source"/> when they are of type <typeparamref name="TType1"/> 
        /// or <typeparamref name="TType2"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TType1">The type of the type1.</typeparam>
        /// <typeparam name="TType2">The type of the type2.</typeparam>
        /// <param name="source">The source.</param>
        /// <returns>Filtered items.</returns>
        public static IReadOnlyList<T> OfTypes<T, TType1, TType2>(this IReadOnlyList<T> source)
            where TType1 : T where TType2 : T
        {
            var result = new List<T>();

            foreach (object obj in source)
            {
                if (obj is TType1 || obj is TType2)
                    result.Add((T)obj);
            }

            return new ReadOnlyCollection<T>(result);
        }
    }
}
