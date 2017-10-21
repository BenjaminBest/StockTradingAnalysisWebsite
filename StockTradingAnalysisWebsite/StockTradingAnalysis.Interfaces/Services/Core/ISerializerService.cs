namespace StockTradingAnalysis.Interfaces.Services
{
    /// <summary>
    /// The interface ISerializerService defines an service which can deserialize a value
    /// </summary>
    public interface ISerializerService
    {
        /// <summary>
        /// Deserializes the given <param name="value">value</param> into the type T.
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="value">Value</param>
        /// <returns></returns>
        T Deserialize<T>(string value);

        /// <summary>
        /// Serializes the given <param name="value">value</param> into a string
        /// </summary>
        /// <param name="value">Value</param>
        /// <returns></returns>
        string Serialize(object value);
    }
}