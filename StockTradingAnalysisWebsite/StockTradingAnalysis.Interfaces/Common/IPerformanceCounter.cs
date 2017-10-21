using StockTradingAnalysis.Interfaces.Enumerations;

namespace StockTradingAnalysis.Interfaces.Common
{
    /// <summary>
    /// Defines an interface for a specific type of performance counter
    /// </summary>
    public interface IPerformanceCounter
    {
        /// <summary>
        /// Gets the performance type of this counter
        /// </summary>
        PerformanceType PerformanceType { get; }

        /// <summary>
        /// Returns the value
        /// </summary>
        /// <returns>The value</returns>
        double GetValue();

        /// <summary>
        /// Increments the value by one
        /// </summary>
        void Increment();

        /// <summary>
        /// Increments the value by the given <paramref name="value"/>
        /// </summary>
        /// <param name="value">The value</param>
        void IncrementBy(long value);

        /// <summary>
        /// Resets the value
        /// </summary>
        void Reset();
    }
}