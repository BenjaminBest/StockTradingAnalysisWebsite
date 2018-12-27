using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace StockTradingAnalysis.Core.Common
{
	/// <summary>
	/// This class provides helper methods for handling with types.
	/// 
	/// </summary>
	public static class TypeHelper
	{
		/// <summary>
		/// Finds the non abstract types with the given <paramref name="assemblyName" />.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="assemblyName">Name of the assembly.</param>
		/// <returns></returns>
		public static List<Type> FindNonAbstractTypes<T>(string assemblyName)
		{
			return FindNonAbstractTypes(assemblyName, typeof(T));
		}

		/// <summary>
		/// Finds the non abstract types with the given type <paramref name="type" />.
		/// </summary>
		/// <param name="assemblyName">Name of the assembly.</param>
		/// <param name="type">The type.</param>
		/// <returns></returns>
		public static List<Type> FindNonAbstractTypes(string assemblyName, Type type)
		{
			return GetTypesFromAllAssemblies(a =>
			{
				//Generic interface is implemented
				if (a.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == type))
					return true;

				//Normal interface
				if (a.IsClass && !a.IsAbstract)
				{
					return type.IsAssignableFrom(a);
				}

				return false;
			}, Assembly.GetEntryAssembly().GetReferencedAssemblies()
				.Concat(AppDomain.CurrentDomain.GetAssemblies().Select(a => a.GetName()))
				.Where(a => a.FullName.StartsWith(assemblyName)).Select(Assembly.Load))
				.ToList();
		}

		/// <summary>
		/// Gets the types from all assemblies.
		/// </summary>
		/// <param name="predicate">The predicate.</param>
		/// <param name="assembliesToSearch">The assemblies to search.</param>
		/// <returns></returns>
		private static IEnumerable<Type> GetTypesFromAllAssemblies(Func<Type, bool> predicate, IEnumerable<Assembly> assembliesToSearch)
		{
			var list = new List<Type>();

			foreach (var assembly in assembliesToSearch)
			{
				list.AddRange(assembly.GetTypes().Where(predicate));
			}

			return list;
		}
	}
}