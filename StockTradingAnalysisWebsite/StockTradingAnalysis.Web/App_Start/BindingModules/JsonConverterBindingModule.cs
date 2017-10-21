using Ninject.Modules;
using Raven.Imports.Newtonsoft.Json;
using StockTradingAnalysis.Web.Converters;

namespace StockTradingAnalysis.Web.BindingModules
{
    public class JsonConverterBindingModule : NinjectModule
    {
        /// <summary>
        /// Loads the module into the kernel.
        /// </summary>
        public override void Load()
        {
            //Services
            Kernel.Bind<JsonConverter>().To<QuotationJsonConverter>();
        }
    }
}