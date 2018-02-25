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
        /// Finds the non abstract types with the given <paramref name="name"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public static List<Type> FindNonAbstractTypes<T>(string name)
        {
            return GetTypesFromAllAssemblies(a =>
            {
                if (a.IsClass && !a.IsAbstract)
                {
                    return typeof(T).IsAssignableFrom(a);
                }

                return false;
            }, AppDomain.CurrentDomain.GetAssemblies().Where(a => a.FullName.StartsWith(name))).ToList();
        }

        /// <summary>
        /// Gets the types from all assemblies.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <param name="assemblysToSearch">The assemblys to search.</param>
        /// <returns></returns>
        private static IEnumerable<Type> GetTypesFromAllAssemblies(Func<Type, bool> predicate,
            IEnumerable<Assembly> assemblysToSearch)
        {
            var list = new List<Type>();

            foreach (var assembly in assemblysToSearch)
            {
                list.AddRange(assembly.GetTypes().Where(predicate));
            }

            return list;
        }
    }
}