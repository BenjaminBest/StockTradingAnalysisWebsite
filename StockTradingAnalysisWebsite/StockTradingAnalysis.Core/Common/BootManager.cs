using System.Collections.Generic;
using StockTradingAnalysis.Interfaces.Common;

namespace StockTradingAnalysis.Core.Common
{
    public class BootManager
    {
        private readonly IEnumerable<IBootModule> _modules;

        public BootManager() : this(new List<IBootModule>())
        {
        }

        public BootManager(IEnumerable<IBootModule> modules)
        {
            _modules = modules;
        }

        public void Boot()
        {
            foreach (var module in _modules)
            {
                module.Boot();
            }
        }
    }
}