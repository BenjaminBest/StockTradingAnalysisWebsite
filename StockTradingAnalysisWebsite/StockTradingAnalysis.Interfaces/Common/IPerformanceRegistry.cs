using StockTradingAnalysis.Interfaces.Enumerations;

namespace StockTradingAnalysis.Interfaces.Common
{
    /// <summary>
    /// The interface IPerformanceRegistry defines a store for all available performance counters
    /// </summary>
    public interface IPerformanceRegistry
    {
        /// <summary>
        /// Registers the given <paramref name="type"/>
        /// </summary>
        /// <param name="type">The performance counter type</param>
        void Register<TPerformanceCounter>(PerformanceType type) where TPerformanceCounter : IPerformanceCounter, new();

        /// <summary>
        /// Creates a new instance of the given <paramref name="type"/>
        /// </summary>
        /// <param name="type">The type of the performance counter</param>
        /// <returns>New instance of the performance counter</returns>
        IPerformanceCounter Create(PerformanceType type);
    }
}