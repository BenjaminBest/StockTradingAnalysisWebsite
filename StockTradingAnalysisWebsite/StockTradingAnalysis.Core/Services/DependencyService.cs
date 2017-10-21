using Ninject;
using StockTradingAnalysis.Interfaces.Services;
using System;
using System.Collections.Generic;

namespace StockTradingAnalysis.Core.Services
{
    /// <summary>
    /// The service DependencyService is a wrapper for the ninject kernel
    /// </summary>
    public class DependencyService : IDependencyService
    {
        private readonly IKernel _kernel;

        /// <summary>
        /// Initializes this object with the ninject kernel
        /// </summary>
        /// <param name="kernel"></param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="kernel" /> is null.</exception>
        public DependencyService(IKernel kernel)
        {
            _kernel = kernel;
        }

        /// <summary>
        /// Resolves singly registered services that support arbitrary object creation.
        /// </summary>
        /// <typeparam name="TType">The type of the requested service or object.</typeparam>
        ///  <returns>The requested service or object.</returns>
        public object GetService<TType>() where TType : Type
        {
            return _kernel.Get(typeof(TType));
        }

        /// <summary>
        /// Resolves multiply registered services.
        /// </summary>
        /// <typeparam name="TType">The type of the requested service or object.</typeparam>
        /// <returns>The requested services.</returns>
        public IEnumerable<object> GetServices<TType>() where TType : Type
        {
            return _kernel.GetAll(typeof(TType));
        }

        /// <summary>
        /// Returns the instance of a object with type <paramref name="type" />
        /// </summary>
        /// <param name="type">The type of the requested service or object.</param>
        /// <returns>Instance of type <paramref name="type" /></returns>
        public object GetService(Type type)
        {
            return _kernel.Get(type);
        }

        /// <summary>
        /// Resolves multiply registered services.
        /// </summary>
        /// <param name="type">The type of the requested service or object.</param>
        /// <returns>The requested services.</returns>
        public IEnumerable<object> GetServices(Type type)
        {
            return _kernel.GetAll(type);
        }
    }
}