using System.Collections.Generic;
using StockTradingAnalysis.Interfaces.Enumerations;

namespace StockTradingAnalysis.Interfaces.Domain
{
    /// <summary>
    /// The interface ITimeSlice defines one single time slice which covers a specific time range.
    /// </summary>
    public interface ITimeSlice : ITimeSliceKey
    {
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