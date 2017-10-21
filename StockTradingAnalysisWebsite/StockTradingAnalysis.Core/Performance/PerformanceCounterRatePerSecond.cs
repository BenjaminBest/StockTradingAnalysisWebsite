using System;
using System.Threading;
using StockTradingAnalysis.Interfaces.Common;
using StockTradingAnalysis.Interfaces.Enumerations;

namespace StockTradingAnalysis.Core.Performance
{
    /// <summary>
    /// Performance counter for counting the rate of operations per second
    /// </summary>
    public class PerformanceCounterRatePerSecond : IPerformanceCounter
    {
        private long _value;
        private long _start;

        /// <summary>
        /// Gets the performance type of this counter
        /// </summary>
        public PerformanceType PerformanceType => PerformanceType.RateOfCountsPerMillisecond;

        /// <summary>
        /// Initializes this object
        /// </summary>
        public PerformanceCounterRatePerSecond()
        {
            Interlocked.Exchange(ref _value, 0);
            Interlocked.Exchange(ref _start, DateTime.Now.ToBinary());
        }

        /// <summary>
        /// Returns the value
        /// </summary>
        /// <returns>The value</returns>
        public double GetValue()
        {
            var start = Interlocked.CompareExchange(ref _start, 0, 0);
            var timeSpan = DateTime.Now - DateTime.FromBinary(start);
            var value = Interlocked.Read(ref _value);

            return value / timeSpan.TotalMilliseconds;
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