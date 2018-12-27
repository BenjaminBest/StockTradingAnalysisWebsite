using Microsoft.Extensions.DependencyInjection;
using StockTradingAnalysis.Core.Common;
using System;

namespace StockTradingAnalysis.Web.Common.Binding
{
	/// <summary>
	/// The class ServiceCollectionExtensions contains extension methods for the ServiceCollection.
	/// </summary>
	public static class ServiceCollectionExtensions
	{
		/// <summary>
		/// The search pattern
		/// </summary>
		private static string _searchPattern = "StockTradingAnalysis.";

		/// <summary>
		/// Binds all services for all interfaces defined by TType.
		/// </summary>
		/// <typeparam name="TType">The type of the type.</typeparam>
		/// <param name="serviceCollection">The service collection.</param>
		public static void AddTransientForAllInterfaces<TType>(this IServiceCollection serviceCollection)
		{
			AddTransientForAllInterfaces(serviceCollection, typeof(TType));
		}

		/// <summary>
		/// Binds all services for all interfaces defined by the given <paramref name="interfaceType"/>.
		/// </summary>
		/// <param name="serviceCollection">The service collection.</param>
		/// <param name="interfaceType">Type of the interface.</param>
		public static void AddTransientForAllInterfaces(this IServiceCollection serviceCollection, Type interfaceType)
		{
			foreach (var type in TypeHelper.FindNonAbstractTypes(_searchPattern, interfaceType))
			{
				foreach (var @interface in type.GetInterfaces())
				{
					serviceCollection.AddTransient(@interface, type);
				}
			}
		}
	}
}