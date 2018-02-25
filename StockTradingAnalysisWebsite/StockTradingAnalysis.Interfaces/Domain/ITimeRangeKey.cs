using System;

namespace StockTradingAnalysis.Interfaces.Domain
{
    /// <summary>
    /// The interface ITimeRangeKey defines a key to store and lookup information for time ranges.
    /// </summary>
    public interface ITimeRangeKey : IEquatable<ITimeRangeKey>
    {
        /// <summary>
        /// Gets the start.
        /// </summary>
        /// <value>
        /// The start.
        /// </value>
        DateTime Start { get; }

        /// <summary>
        /// Gets the end.
        /// </summary>
        /// <value>
        /// The end.
        /// </value>
        DateTime End { get; }
    }
}