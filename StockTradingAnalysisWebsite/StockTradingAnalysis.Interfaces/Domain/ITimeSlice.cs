using System;
using System.Collections.Generic;
using StockTradingAnalysis.Interfaces.Enumerations;

namespace StockTradingAnalysis.Interfaces.Domain
{
    /// <summary>
    /// The interface ITimeSlice defines one single time slice which covers a specific time range.
    /// </summary>
    public interface ITimeSlice
    {
        /// <summary>
        /// Gets or sets the start.
        /// </summary>
        /// <value>
        /// The start.
        /// </value>
        DateTime Start { get; set; }

        /// <summary>
        /// Gets or sets the end.
        /// </summary>
        /// <value>
        /// The end.
        /// </value>
        DateTime End { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        TimeSliceType Type { get; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        int Value { get; set; }

        /// <summary>
        /// Gets all slices.
        /// </summary>
        /// <returns></returns>
        IEnumerable<ITimeSlice> GetAllSlices();
    }
}