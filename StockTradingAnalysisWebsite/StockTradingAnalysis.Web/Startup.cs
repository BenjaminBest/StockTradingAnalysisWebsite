using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using React.AspNet;
using StockTradingAnalysis.Core.Common;
using StockTradingAnalysis.Interfaces.Common;
using StockTradingAnalysis.Interfaces.Scheduler;
using StockTradingAnalysis.Web.Common.Authorization;
using StockTradingAnalysis.Web.Common.Binding;
using System;
using System.Linq;

namespace StockTradingAnalysis.Web
{
	public class Startup
	{
		/// <summary>
		/// The configuration
		/// </summary>
		private readonly IConfiguration _configuration;

		/// <summary>
		/// Initializes a new instance of the <see cref="Startup"/> class.
		/// </summary>
		/// <param name="configuration">The configuration.</param>
		public Startup(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		// This method gets called by the runtime. Use this method to add services to the container.
		// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
		public IServiceProvider ConfigureServices(IServiceCollection services)
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

			//Hangfire
			//TODO: Support RavenDB as well
			services.AddHangfire(config =>
			{
				config.UseSqlServerStorage(_configuration.GetConnectionString("StockTradingAnalysis_MSSQL"));
				config.UseLog4NetLogProvider();
			});

			//DI configuration
			new BindingModulesManager(services).Load();

			var serviceProvider = services.BuildServiceProvider();
			DependencyResolver.Init(new ServiceProviderWrapper(serviceProvider));

			//Booting configuration modules
			var bootModules = serviceProvider.GetServices<IBootModule>().OrderByDescending(m => m.Priority);
			var bootManager = new BootManager(bootModules);
			bootManager.Boot();

			return serviceProvider;
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

			// Initialise ReactJS.NET. Must be before static files. See for more: http://reactjs.net/
			app.UseReact(config => { });

			app.UseStaticFiles();
			app.UseCookiePolicy();

			app.UseHangfireDashboard("/scheduler", new DashboardOptions
			{
				Authorization = new[] { new HangfireAuthorizationFilter() }
			});
			app.UseHangfireServer();

			//Register jobs
			foreach (var scheduledJob in DependencyResolver.Current.GetServices<IScheduledJob>())
			{
				RecurringJob.AddOrUpdate(() => scheduledJob.Execute(), scheduledJob.CronExpression);
			}

			//Routes
			app.UseMvc(routes =>
			{
				routes.MapRoute("default", "{controller=Dashboard}/{action=Index}/{id?}");
			});
		}
	}
}
