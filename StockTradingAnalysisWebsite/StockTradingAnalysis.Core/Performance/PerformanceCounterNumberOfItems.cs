using System.Threading;
using StockTradingAnalysis.Interfaces.Common;
using StockTradingAnalysis.Interfaces.Enumerations;

namespace StockTradingAnalysis.Core.Performance
{
    /// <summary>
    /// Performance counter for counting the total amount of items
    /// </summary>
    public class PerformanceCounterNumberOfItems : IPerformanceCounter
    {
        private long _value;

        /// <summary>
        /// Gets the performance type of this counter
        /// </summary>
        public PerformanceType PerformanceType => PerformanceType.NumberOfItems;

        /// <summary>
        /// Initializes this object
        /// </summary>
        public PerformanceCounterNumberOfItems()
        {
            Interlocked.Exchange(ref _value, 0);
        }

        /// <summary>
        /// Returns the value
        /// </summary>
        /// <returns>The value</returns>
        public double GetValue()
        {
            return Interlocked.Read(ref _value);
        }

        /// <summary>
        /// Increments the value by one
        /// </summary>
        public void Increment()
        {
            Interlocked.Increment(ref _value);
        }

        /// <summary>
        /// Increments the value by the given <paramref name="value"/>
        /// </summary>
        /// <param name="value">The value</param>
        public void IncrementBy(long value)
        {
            Interlocked.Add(ref _value, value);
        }

        /// <summary>
        /// Resets the value
        /// </summary>
        public void Reset()
        {
            Interlocked.Exchange(ref _value, 0);
        }
    }
}