using System;
using StockTradingAnalysis.Interfaces.Domain;

namespace StockTradingAnalysis.Interfaces.Services.Domain
{
    /// <summary>
    /// The interface ITimeSliceCreationService defines a service to create all time slices affected by all executed transactions.
    /// </summary>
    public interface ITimeSliceCreationService
    {
        /// <summary>
        /// Creates all time slices.
        /// </summary>
        /// <returns></returns>
        ITimeSlice CreateTimeSlices();

        /// <summary>
        /// Creates the time slices which are affected by the given <paramref name="date"/>
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns></returns>
        ITimeSlice CreateTimeSlices(DateTime date);
    }
}