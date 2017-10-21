using System;
using System.Threading;
using StockTradingAnalysis.Interfaces.Common;
using StockTradingAnalysis.Interfaces.Enumerations;

namespace StockTradingAnalysis.Core.Performance
{
    /// <summary>
    /// Performance counter for counting the average time needed for a operation
    /// </summary>
    public class PerformanceCounterAverageTimer : IPerformanceCounter
    {
        private long _value;
        private long _count;

        /// <summary>
        /// Gets the performance type of this counter
        /// </summary>
        public PerformanceType PerformanceType => PerformanceType.AverageTimer;

        /// <summary>
        /// Initializes this object
        /// </summary>
        public PerformanceCounterAverageTimer()
        {
            Interlocked.Exchange(ref _value, 0);
            Interlocked.Exchange(ref _count, 0);
        }

        /// <summary>
        /// Returns the value
        /// </summary>
        /// <returns>The value</returns>
        public double GetValue()
        {
            var count = Interlocked.Read(ref _count);
            var value = Interlocked.Read(ref _value);

            if (count == 0)
                return 0;

            return Convert.ToDouble(value) / Convert.ToDouble(count);
        }

        /// <summary>
        /// Increments the value by one
        /// </summary>
        public void Increment()
        {
            throw new NotSupportedException("Increment is not supported by this performance counter. Use milliseconds to increment.");
        }

        /// <summary>
        /// Increments the value by the given <paramref name="value"/>
        /// </summary>
        /// <param name="value">The value</param>
        public void IncrementBy(long value)
        {
            Interlocked.Add(ref _value, value);
            Interlocked.Increment(ref _count);
        }

        /// <summary>
        /// Resets the value
        /// </summary>
        public void Reset()
        {
            Interlocked.Exchange(ref _value, 0);
            Interlocked.Exchange(ref _count, 0);
        }
    }
}