using System.Collections.Generic;
using Ninject;

namespace StockTradingAnalysis.Core.Common
{
    /// <summary>
    /// The DependencyResolver is a wrapper for the ninject kernel
    /// </summary>
    public class DependencyResolver
    {
        private static IKernel _kernel;

        //static DependencyResolver()
        //{
        //    _kernel = new StandardKernel();
        //    try
        //    {
        //        _kernel.Load("StockTradingAnalysis.Web.dll"); //TODO: Load from configuration
        //    }
        //    catch
        //    {
        //        _kernel.Dispose();
        //        throw;
        //    }
        //}

        public static void SetKernel(IKernel kernel)
        {
            _kernel = kernel;
        }

        /// <summary>
        /// Resolves singly registered services that support arbitrary object creation.
        /// </summary>
        /// <typeparam name="TType">The type of the requested service or object.</typeparam>
        ///  <returns>The requested service or object.</returns>
        public static TType GetService<TType>()
        {
            return (TType)_kernel.TryGet(typeof(TType));
        }

        /// <summary>
        /// Resolves multiply registered services.
        /// </summary>
        /// <typeparam name="TType">The type of the requested service or object.</typeparam>
        /// <returns>The requested services.</returns>
        public static IEnumerable<TType> GetServices<TType>()
        {
            return (IEnumerable<TType>)_kernel.GetAll(typeof(TType));
        }
    }
}