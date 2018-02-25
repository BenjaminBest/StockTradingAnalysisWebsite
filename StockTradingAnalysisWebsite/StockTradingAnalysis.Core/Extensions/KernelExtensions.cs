using System;
using Ninject;
using Ninject.Extensions.Conventions;
using Ninject.Extensions.Conventions.Syntax;

namespace StockTradingAnalysis.Core.Extensions
{
    /// <summary>
    /// The class KernelExtensions contains extension methods for Kernal or IKernal
    /// </summary>
    public static class KernelExtensions
    {
        /// <summary>
        /// Finds the type of all interfaces of type <paramref name="interfaceTypes" /> with the given assemply search pattern
        /// <paramref name="assemblySearchPattern" />
        /// </summary>
        /// <param name="syntax">The syntax.</param>
        /// <param name="assemblySearchPattern">The assembly search pattern.</param>
        /// <param name="interfaceTypes">The interface types.</param>
        /// <returns></returns>
        public static IJoinFilterWhereExcludeIncludeBindSyntax FindAllInterfacesOfType(this IFromSyntax syntax, string assemblySearchPattern, Type interfaceTypes)
        {
            return syntax
                .FromAssembliesMatching(assemblySearchPattern)
                .SelectAllClasses()
                .InheritedFrom(interfaceTypes);
        }

        /// <summary>
        /// Finds the type of all interfaces of type <paramref name="interfaceTypes" /> with the given assemply search pattern
        /// <paramref name="assemblySearchPattern" /> and binds them
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        /// <param name="assemblySearchPattern">The assembly search pattern.</param>
        /// <param name="interfaceTypes">The interface types.</param>
        public static void FindAllInterfacesOfType(this IKernel kernel, string assemblySearchPattern, Type interfaceTypes)
        {
            kernel.Bind(i => i
                .FromAssembliesMatching(assemblySearchPattern)
                .SelectAllClasses()
                .InheritedFrom(interfaceTypes)
                .BindAllInterfaces());
        }
    }
}
