using System;
using System.Collections.Generic;

namespace StockTradingAnalysis.Interfaces.DI
{
	public interface IServiceProviderWrapper
	{
		/// <summary>
		/// Gets the service.
		/// </summary>
		/// <typeparam name="TType">The type of the type.</typeparam>
		/// <returns></returns>
		TType GetService<TType>();

		/// <summary>
		/// Gets the services.
		/// </summary>
		/// <typeparam name="TType">The type of the type.</typeparam>
		/// <returns></returns>
		IEnumerable<TType> GetServices<TType>();

		/// <summary>
		/// Gets the services.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <returns></returns>
		IEnumerable<object> GetServices(Type type);

		/// <summary>
		/// Gets the service.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <returns></returns>
		object GetService(Type type);
	}
}