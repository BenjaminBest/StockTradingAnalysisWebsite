using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace StockTradingAnalysis.Core.Common
{
    /// <summary>
    /// This class provides helper methods for handling with types
    /// 
    /// </summary>
    public static class TypeHelper
    {
        public static List<Type> FindNonAbstractTypes<T>(string name)
        {
            return GetTypesFromAllAssemblies(a =>
            {
                if (a.IsClass && !a.IsAbstract)
                {
                    return typeof (T).IsAssignableFrom(a);
                }

                return false;
            }, AppDomain.CurrentDomain.GetAssemblies().Where(a => a.FullName.StartsWith(name))).ToList();
        }

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