namespace StockTradingAnalysis.Interfaces.Configuration
{
    /// <summary>
    /// The interface IConfigurationRegistry defines a store for configuration values based on a given key
    /// </summary>
    public interface IConfigurationRegistry
    {
        /// <summary>
        /// Adds the given <paramref name="value"/> with the key <paramref name="key"/> to the configuration
        /// </summary>
        /// <typeparam name="TType">The type of the value</typeparam>
        /// <param name="key">The key</param>
        /// <param name="value">The value</param>
        void AddValue<TType>(string key, TType value);

        /// <summary>
        /// Get the value stored with the given key <paramref name="key"/>
        /// </summary>
        /// <typeparam name="TType">The type of the value</typeparam>
        /// <param name="key">The key</param>
        /// <returns><c>Null</c> if no value could be fould</returns>
        TType GetValue<TType>(string key);
    }
}