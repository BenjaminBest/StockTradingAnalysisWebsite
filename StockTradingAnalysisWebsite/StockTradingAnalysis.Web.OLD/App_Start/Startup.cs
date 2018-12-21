using Hangfire;
using Microsoft.Owin;
using Owin;
using Raven.Abstractions.Extensions;
using StockTradingAnalysis.Core.Common;
using StockTradingAnalysis.Interfaces.Scheduler;
using StockTradingAnalysis.Web;
using StockTradingAnalysis.Web.Common.Authorization;

[assembly: OwinStartup(typeof(Startup), "Configuration")]
namespace StockTradingAnalysis.Web
{
	public class Startup
	{
		public void Configuration(IAppBuilder app)
		{
			app.MapSignalR();

			//Hangfire
			//TODO: Support RavenDB as well
			GlobalConfiguration.Configuration
				.UseSqlServerStorage("StockTradingAnalysis_MSSQL")
				.UseLog4NetLogProvider();

			app.UseHangfireDashboard("/scheduler", new DashboardOptions
			{
				Authorization = new[] { new HangfireAuthorizationFilter()}
			});
			app.UseHangfireServer();

			//Register jobs
			DependencyResolver.GetServices<IScheduledJob>()
				.ForEach(j => RecurringJob.AddOrUpdate(() => j.Execute(), j.CronExpression));
		}
	}
}