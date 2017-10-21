using Ninject;
using Ninject.Web.Common;
using StockTradingAnalysis.Core.Common;
using StockTradingAnalysis.Interfaces.Common;
using StockTradingAnalysis.Interfaces.Data;
using StockTradingAnalysis.Web.BootModules;
using System;

namespace StockTradingAnalysis.Web.Migration
{
    public static class Initialize
    {
        /// <summary>
        /// Initializes the application
        /// </summary>
        public static void Start()
        {
            //Booting configuration modules
            new LoggingBootModule().Boot();
            new AutoMapperBootModule().Boot();

            CreateKernel();

            //Erase Database
            DependencyResolver.GetService<IEventDatastore>().DeleteAll();
            DependencyResolver.GetService<ISnapshotDatastore>().DeleteAll();
        }

        /// <summary>
        /// Returns the logging service
        /// </summary>
        /// <returns></returns>
        public static ILoggingService GetLogger()
        {
            return DependencyResolver.GetService<ILoggingService>();
        }

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static void CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Load("StockTradingAnalysis.Web.dll");

                DependencyResolver.SetKernel(kernel);
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        public static void Stop()
        {
        }
    }
}