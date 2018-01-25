using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace StockTradingAnalysis.Data.MSSQL
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="DefaultContractResolver" />
    public class JsonPrivateSetterContractResolver : DefaultContractResolver
    {
        protected JsonPrivateSetterContractResolver()
        { }

        // "Use the parameterless constructor and cache instances of the contract resolver within your application for optimal performance."
        // Using an explicit static constructor enables lazy initialization.
        static JsonPrivateSetterContractResolver() { Instance = new JsonPrivateSetterContractResolver(); }

        public static JsonPrivateSetterContractResolver Instance { get; }

        /// <summary>
        /// Creates a <see cref="T:Newtonsoft.Json.Serialization.JsonProperty" /> for the given <see cref="T:System.Reflection.MemberInfo" />.
        /// </summary>
        /// <param name="member">The member to create a <see cref="T:Newtonsoft.Json.Serialization.JsonProperty" /> for.</param>
        /// <param name="memberSerialization">The member's parent <see cref="T:Newtonsoft.Json.MemberSerialization" />.</param>
        /// <returns>
        /// A created <see cref="T:Newtonsoft.Json.Serialization.JsonProperty" /> for the given <see cref="T:System.Reflection.MemberInfo" />.
        /// </returns>
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            var prop = base.CreateProperty(member, memberSerialization);

            if (prop.Writable)
                return prop;

            prop.Writable = (member as PropertyInfo)?.GetSetMethod(true) != null;

            return prop;
        }
    }
}
