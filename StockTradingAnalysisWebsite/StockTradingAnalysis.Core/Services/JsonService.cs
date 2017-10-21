using Newtonsoft.Json;
using StockTradingAnalysis.Interfaces.Services;

namespace StockTradingAnalysis.Core.Services
{
    /// <summary>
    /// The class JsonSerializerService defines an service which can deserialize a value
    /// </summary>
    public class JsonSerializerService : ISerializerService
    {
        /// <summary>
        /// Deserializes the given <param name="value">value</param> into the type T.
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="value">Value</param>
        /// <returns></returns>
        public T Deserialize<T>(string value)
        {
            return string.IsNullOrEmpty(value) ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }

        /// <summary>
        /// Serializes the given <param name="value">value</param> into a string
        /// </summary>
        /// <param name="value">Value</param>
        /// <returns></returns>
        public string Serialize(object value)
        {
            return value == null ? string.Empty : JsonConvert.SerializeObject(value);
        }
    }
}
