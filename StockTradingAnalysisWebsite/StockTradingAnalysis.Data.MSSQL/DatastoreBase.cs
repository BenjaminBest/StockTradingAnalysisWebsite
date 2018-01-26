using Newtonsoft.Json;
using StockTradingAnalysis.Core.Common;
using StockTradingAnalysis.Interfaces.Services.Core;

namespace StockTradingAnalysis.Data.MSSQL
{
    /// <summary>
    /// Base class for the datastores
    /// </summary>
    public class DatastoreBase
    {
        /// <summary>
        /// The performance measurement service
        /// </summary>
        private readonly IPerformanceMeasurementService _performanceMeasurementService;

        /// <summary>
        /// Initializes a new instance of the <see cref="DatastoreBase"/> class.
        /// </summary>
        /// <param name="performanceMeasurementService">The performance measurement service.</param>
        public DatastoreBase(IPerformanceMeasurementService performanceMeasurementService)
        {
            _performanceMeasurementService = performanceMeasurementService;
        }

        /// <summary>
        /// Deserializes the given <param name="value">value</param> into the type T.
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="value">Value</param>
        /// <returns></returns>
        internal T Deserialize<T>(string value)
        {
            using (new TimeMeasure(ms => _performanceMeasurementService.CountDeserializedObjects(ms)))
            {
                var settings = new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.All,
                    ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
                    ContractResolver = JsonPrivateSetterContractResolver.Instance
                };

                return string.IsNullOrEmpty(value) ? default(T) : (T)JsonConvert.DeserializeObject(value, settings);
            }
        }

        /// <summary>
        /// Serializes the given <param name="value">value</param> into a string
        /// </summary>
        /// <param name="value">Value</param>
        /// <returns></returns>
        internal string Serialize(object value)
        {
            using (new TimeMeasure(ms => _performanceMeasurementService.CountSerializedObjects(ms)))
            {
                var settings = new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.All,
                    ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
                    ContractResolver = JsonPrivateSetterContractResolver.Instance
                };

                return value == null ? string.Empty : JsonConvert.SerializeObject(value, settings);
            }
        }
    }
}
