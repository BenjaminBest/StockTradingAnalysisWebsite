using System;
using StockTradingAnalysis.Interfaces.Enumerations;

namespace StockTradingAnalysis.Interfaces.Domain
{
    /// <summary>
    /// The interface ITimeSliceKey defines a key to store and lookup information for time ranges.
    /// </summary>
    public interface ITimeSliceKey : IEquatable<ITimeSliceKey>
    {
        /// <summary>
        /// Gets or sets the start.
        /// </summary>
        /// <value>
        /// The start.
        /// </value>
        DateTime Start { get; }

        /// <summary>
        /// Gets or sets the end.
        /// </summary>
        /// <value>
        /// The end.
        /// </value>
        DateTime End { get; }

        /// <summary>
        /// Gets the timeslice type.
        /// </summary>
        TimeSliceType Type { get; }
    }
}