using System;
using System.Collections.Generic;
using StockTradingAnalysis.Interfaces.Common;
using StockTradingAnalysis.Interfaces.Enumerations;

namespace StockTradingAnalysis.Core.Performance
{
    /// <summary>
    /// The PerformanceRegistry stores all available performance counters
    /// </summary>
    public class PerformanceRegistry : IPerformanceRegistry
    {
        private readonly IDictionary<PerformanceType, Type> _counters = new Dictionary<PerformanceType, Type>();

        /// <summary>
        /// Registers the given <paramref name="type"/>
        /// </summary>
        /// <param name="type">The performance counter type</param>
        public void Register<TPerformanceCounter>(PerformanceType type) where TPerformanceCounter : IPerformanceCounter, new()
        {
            if (!_counters.ContainsKey(type))
                _counters.Add(type, typeof(TPerformanceCounter));

        }

        /// <summary>
        /// Creates a new instance of the given <paramref name="type"/>
        /// </summary>
        /// <param name="type">The type of the performance counter</param>
        /// <returns>New instance of the performance counter</returns>
        public IPerformanceCounter Create(PerformanceType type)
        {
            Type performanceCounterType;

            if (_counters.TryGetValue(type, out performanceCounterType))
                return (IPerformanceCounter)Activator.CreateInstance(performanceCounterType);

            throw new InvalidOperationException($"Could not find a performance counter which supports the type {type}");
        }
    }
}