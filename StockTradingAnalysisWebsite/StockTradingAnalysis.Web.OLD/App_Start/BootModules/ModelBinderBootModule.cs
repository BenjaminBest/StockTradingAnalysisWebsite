using System.Web.Mvc;
using StockTradingAnalysis.Interfaces.Common;
using StockTradingAnalysis.Web.Common.ModelBinders;
using StockTradingAnalysis.Web.Models;

namespace StockTradingAnalysis.Web.BootModules
{
    /// <summary>
    /// Defines an boot module for configuration purposes
    /// </summary>
    public class ModelBinderBootModule : IBootModule
    {
        /// <summary>
        /// Gets the priority
        /// </summary>
        public int Priority => 0;

        /// <summary>
        /// Boots up the module
        /// </summary>
        public void Boot()
        {
            ModelBinders.Binders.Add(typeof(ImageViewModel), new ImageViewModelBinder());
        }
    }
}