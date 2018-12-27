using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using StockTradingAnalysis.Interfaces.DI;

namespace StockTradingAnalysis.Web.Common.Binding
{
	/// <summary>
	/// The ServiceProviderWrapper is a wrapper for <see cref="IServiceProvider"/> to be able to mock the static methods.
	/// </summary>
	/// <seealso cref="IServiceProviderWrapper" />
	public class ServiceProviderWrapper : IServiceProviderWrapper
	{
		/// <summary>
		/// The service provider
		/// </summary>
		private readonly IServiceProvider _serviceProvider;

		/// <summary>
		/// Initializes a new instance of the <see cref="ServiceProviderWrapper"/> class.
		/// </summary>
		/// <param name="serviceProvider">The service provider.</param>
		public ServiceProviderWrapper(IServiceProvider serviceProvider)
		{
			_serviceProvider = serviceProvider;
		}

		/// <summary>
		/// Gets the service.
		/// </summary>
		/// <typeparam name="TType">The type of the type.</typeparam>
		/// <returns></returns>
		public TType GetService<TType>()
		{
			return _serviceProvider.GetService<TType>();
		}

		/// <summary>
		/// Gets the services.
		/// </summary>
		/// <typeparam name="TType">The type of the type.</typeparam>
		/// <returns></returns>
		public IEnumerable<TType> GetServices<TType>()
		{
			return _serviceProvider.GetServices<TType>();
		}

		/// <summary>
		/// Gets the services.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <returns></returns>
		public IEnumerable<object> GetServices(Type type)
		{
			return _serviceProvider.GetServices(type);
		}

		/// <summary>
		/// Gets the service.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <returns></returns>
		public object GetService(Type type)
		{
			return _serviceProvider.GetService(type);
		}
	}
}
