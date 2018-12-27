using System.Collections.Generic;
using StockTradingAnalysis.Interfaces.Common;

namespace StockTradingAnalysis.Core.Common
{
	/// <summary>
	/// The boot manager gets all boot modules and starts them.
	/// </summary>
	public class BootManager
	{
		/// <summary>
		/// The modules
		/// </summary>
		private readonly IEnumerable<IBootModule> _modules;

		/// <summary>
		/// Initializes a new instance of the <see cref="BootManager"/> class.
		/// </summary>
		public BootManager() : this(new List<IBootModule>())
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="BootManager"/> class.
		/// </summary>
		/// <param name="modules">The modules.</param>
		public BootManager(IEnumerable<IBootModule> modules)
		{
			_modules = modules;
		}

		/// <summary>
		/// Boots this instance.
		/// </summary>
		public void Boot()
		{
			foreach (var module in _modules)
			{
				module.Boot();
			}
		}
	}
}