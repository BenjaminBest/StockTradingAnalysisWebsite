using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using StockTradingAnalysis.Interfaces.DI;

namespace StockTradingAnalysis.Core.Common
{
	/// <summary>
	/// The DependencyResolver is a wrapper for ASP.NET Core internal DI system.
	/// </summary>
	public class DependencyResolver
	{
		/// <summary>
		/// The resolver
		/// </summary>
		private static DependencyResolver _resolver;

		/// <summary>
		/// The service provider
		/// </summary>
		private readonly IServiceProviderWrapper _serviceProvider;

		/// <summary>
		/// Gets the current.
		/// </summary>
		/// <value>
		/// The current.
		/// </value>
		/// <exception cref="Exception">AppDependencyResolver not initialized. You should initialize it in Startup class</exception>
		public static DependencyResolver Current
		{
			get
			{
				if (_resolver == null)
					throw new Exception("AppDependencyResolver not initialized. You should initialize it in Startup class");
				return _resolver;
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="DependencyResolver"/> class.
		/// </summary>
		/// <param name="serviceProvider">The service provider.</param>
		private DependencyResolver(IServiceProviderWrapper serviceProvider)
		{
			_serviceProvider = serviceProvider;
		}

		/// <summary>
		/// Initializes the specified services.
		/// </summary>
		/// <param name="services">The services.</param>
		public static void Init(IServiceProviderWrapper services)
		{
			_resolver = new DependencyResolver(services);
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
		/// Gets the service.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <returns></returns>
		public object GetService(Type type)
		{
			return _serviceProvider.GetService(type);
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
	}
}