using System;
using System.Collections.Generic;

namespace StockTradingAnalysis.Interfaces.Services
{
    /// <summary>
    /// Defines an interface which wraps the ninject kernel to resolve instanced for specific types
    /// </summary>
    public interface IDependencyService
    {
        /// <summary>
        /// Resolves singly registered services that support arbitrary object creation.
        /// </summary>
        /// <typeparam name="TType">The type of the requested service or object.</typeparam>
        /// <returns>The requested service or object.</returns>
        object GetService<TType>();

        /// <summary>
        /// Resolves multiply registered services.
        /// </summary>
        /// <typeparam name="TType">The type of the requested service or object.</typeparam>
        /// <returns>The requested services.</returns>
        IEnumerable<object> GetServices<TType>();

        /// <summary>
        /// Returns the instance of a object with type <paramref name="type"/>
        /// </summary>
        /// <param name="type">The type of the requested service or object.</param>
        /// <returns>Instance of type <paramref name="type"/></returns>
        object GetService(Type type);

        /// <summary>
        /// Resolves multiply registered services.
        /// </summary>
        /// <param name="type">The type of the requested service or object.</param>
        /// <returns>The requested services.</returns>
        IEnumerable<object> GetServices(Type type);
    }
}