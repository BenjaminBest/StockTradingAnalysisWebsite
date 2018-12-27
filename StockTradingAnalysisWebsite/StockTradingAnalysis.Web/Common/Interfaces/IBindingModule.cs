using Microsoft.Extensions.DependencyInjection;

namespace StockTradingAnalysis.Web.Common.Interfaces
{
	/// <summary>
	/// The interface IBindingModule defines a module which when loaded binds types for DI.
	/// </summary>
	public interface IBindingModule
	{
		/// <summary>
		/// Loads the binding configuration.
		/// </summary>
		/// <param name="serviceCollection">The service collection.</param>
		void Load(IServiceCollection serviceCollection);
	}
}