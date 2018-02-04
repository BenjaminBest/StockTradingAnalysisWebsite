using System.Collections.Generic;
using StockTradingAnalysis.Interfaces.Configuration;

namespace StockTradingAnalysis.Core.Configuration
{
    /// <summary>
    /// Class ConfigurationRegistry stores configuration values based on a given key
    /// </summary>
    public class ConfigurationRegistry : IConfigurationRegistry
    {
        private readonly Dictionary<string, object> _configuration = new Dictionary<string, object>();

        /// <summary>
        /// Adds the given <paramref name="value"/> with the key <paramref name="key"/> to the configuration
        /// </summary>
        /// <typeparam name="TType">The type of the value</typeparam>
        /// <param name="key">The key</param>
        /// <param name="value">The value</param>
        public void AddValue<TType>(string key, TType value)
        {
            if (string.IsNullOrEmpty(key))
                return;

            if (value == null)
                return;

            _configuration.Add(key, value);
        }

        /// <summary>
        /// Get the value stored with the given key <paramref name="key"/>
        /// </summary>
        /// <typeparam name="TType">The type of the value</typeparam>
        /// <param name="key">The key</param>
        /// <returns><c>Null</c> if no value could be fould</returns>
        public TType GetValue<TType>(string key)
        {
            if (string.IsNullOrEmpty(key))
                return default(TType);

            if (!_configuration.ContainsKey(key))
                return default(TType);

            var value = _configuration[key];

            return (TType)value;
        }

    }
}
