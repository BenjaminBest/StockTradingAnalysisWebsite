namespace StockTradingAnalysis.Interfaces.Common
{
    /// <summary>
    /// The interface IPerformanceCounterItem defines a specific performance result
    /// </summary>
    public interface IPerformanceCounterItem
    {
        /// <summary>
        /// Gets the key
        /// </summary>
        string Key { get; }

        /// <summary>
        /// Gets the value
        /// </summary>
        double Value { get; }

        /// <summary>
        /// Gets a formatted string of key and value
        /// </summary>
        string Display { get; }
    }
}