using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using React.AspNet;
using StockTradingAnalysis.Core.Common;
using StockTradingAnalysis.Interfaces.Scheduler;
using StockTradingAnalysis.Web.Common.Authorization;

namespace StockTradingAnalysis.Web
{
	public class Startup
	{
		// This method gets called by the runtime. Use this method to add services to the container.
		// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
		public void ConfigureServices(IServiceCollection services)
		{
			services.Configure<CookiePolicyOptions>(options =>
			{
				// This lambda determines whether user consent for non-essential cookies is needed for a given request.
				options.CheckConsentNeeded = context => true;
				options.MinimumSameSitePolicy = SameSiteMode.None;
			});

			services.AddSignalR();
			services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
			services.AddReact(); //https://reactjs.net/getting-started/aspnetcore.html

			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();

			// Initialise ReactJS.NET. Must be before static files.
			app.UseReact(config =>
			{
				// If you want to use server-side rendering of React components,
				// add all the necessary JavaScript files here. This includes
				// your components as well as all of their dependencies.
				// See http://reactjs.net/ for more information. Example:
				//config
				//  .AddScript("~/Scripts/First.jsx")
				//  .AddScript("~/Scripts/Second.jsx");

				// If you use an external build too (for example, Babel, Webpack,
				// Browserify or Gulp), you can improve performance by disabling
				// ReactJS.NET's version of Babel and loading the pre-transpiled
				// scripts. Example:
				//config
				//  .SetLoadBabel(false)
				//  .AddScriptWithoutTransform("~/Scripts/bundle.server.js");
			});

			app.UseStaticFiles();
			app.UseCookiePolicy();

			//Hangfire
			//TODO: Support RavenDB as well
			GlobalConfiguration.Configuration
				.UseSqlServerStorage("StockTradingAnalysis_MSSQL")
				.UseLog4NetLogProvider();

			app.UseHangfireDashboard("/scheduler", new DashboardOptions
			{
				Authorization = new[] { new HangfireAuthorizationFilter() }
			});
			app.UseHangfireServer();

			//Register jobs
			foreach (var scheduledJob in DependencyResolver.GetServices<IScheduledJob>())
			{
				RecurringJob.AddOrUpdate(() => scheduledJob.Execute(), scheduledJob.CronExpression);
			}

			//Routes
			app.UseMvc(routes =>
			{
				routes.MapRoute(
					name: "default",
					template: "{controller=Dashboard}/{action=Index}/{id?}");
			});
		}
	}
}
