using Ninject.Modules;
using Raven.Imports.Newtonsoft.Json;
using StockTradingAnalysis.Web.Converters;

namespace StockTradingAnalysis.Web.BindingModules
{
    /// <summary>
    /// Binding module for json converters.
    /// </summary>
    /// <seealso cref="NinjectModule" />
    public class JsonConverterBindingModule : NinjectModule
    {
        /// <summary>
        /// Loads the module into the kernel.
        /// </summary>
        public override void Load()
        {
            //Services
            Bind<JsonConverter>().To<QuotationJsonConverter>();
        }
    }
}