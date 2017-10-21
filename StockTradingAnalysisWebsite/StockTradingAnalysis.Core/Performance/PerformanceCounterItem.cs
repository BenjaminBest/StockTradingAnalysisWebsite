using System.Diagnostics;
using StockTradingAnalysis.Interfaces.Common;

namespace StockTradingAnalysis.Core.Performance
{
    [DebuggerDisplay("{Key}: {Value}")]
    public class PerformanceCounterItem : IPerformanceCounterItem
    {
        /// <summary>
        /// Gets the key
        /// </summary>
        public string Key { get; }

        /// <summary>
        /// Gets the value
        /// </summary>
        public double Value { get; }

        /// <summary>
        /// Gets a formatted string of key and value
        /// </summary>
        public string Display => $"{Key}: {Value}";

        /// <summary>
        /// Initializes this object
        /// </summary>
        /// <param name="key">The key</param>
        /// <param name="value">The value</param>        
        public PerformanceCounterItem(string key, double value)
        {
            Key = key;
            Value = value;
        }
    }
}
