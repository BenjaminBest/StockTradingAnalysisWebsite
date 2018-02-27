using StockTradingAnalysis.Interfaces.Domain;

namespace StockTradingAnalysis.Interfaces.Services.Domain
{
    /// <summary>
    /// THe interface IStatisticService defines a service to calculate time range based statistical information for transational data
    /// </summary>
    public interface IStatisticService
    {
        /// <summary>
        /// Starts calculation for the given time slice
        /// </summary>
        /// <param name="timeRange">The time range.</param>
        /// <returns></returns>
        IStatistic Calculate(ITimeSlice timeRange);
    }
}