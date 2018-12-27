using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using StockTradingAnalysis.Core.Common;
using StockTradingAnalysis.Interfaces.Common;
using StockTradingAnalysis.Web.Common.Interfaces;

namespace StockTradingAnalysis.Web.Common.Binding
{
	/// <summary>
	/// The boot manager gets all boot modules and starts them.
	/// </summary>
	public class BindingModulesManager
	{
		/// <summary>
		/// The service collection
		/// </summary>
		private readonly IServiceCollection _serviceCollection;

		/// <summary>
		/// The modules
		/// </summary>
		protected IEnumerable<IBindingModule> Modules;

		/// <summary>
		/// Initializes a new instance of the <see cref="BootManager" /> class.
		/// </summary>
		/// <param name="serviceCollection">The service collection.</param>
		public BindingModulesManager(IServiceCollection serviceCollection)
		{
			_serviceCollection = serviceCollection;
		}

		/// <summary>
		/// Initializes this instance.
		/// </summary>
		protected void Initialize()
		{
			Modules = TypeHelper.FindNonAbstractTypes<IBindingModule>("StockTradingAnalysis.")
				.Select(Activator.CreateInstance).Cast<IBindingModule>();
		}

		/// <summary>
		/// Boots this instance.
		/// </summary>
		public void Load()
		{
			Initialize();

			foreach (var module in Modules)
			{
				module.Load(_serviceCollection);
			}
		}
	}
}
