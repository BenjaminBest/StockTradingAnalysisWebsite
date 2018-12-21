using Hangfire.Dashboard;
using Microsoft.Owin;
using Microsoft.Owin.Security.Provider;

namespace StockTradingAnalysis.Web.Common.Authorization
{
	/// <summary>
	/// The HangfireAuthorizationFilter authorizes access from localhost or authenticated users.
	/// </summary>
	/// <seealso cref="Hangfire.Dashboard.IDashboardAuthorizationFilter" />
	public class HangfireAuthorizationFilter: IDashboardAuthorizationFilter
	{
		/// <summary>
		/// Authorizes the specified context.
		/// </summary>
		/// <param name="context">The context.</param>
		/// <returns></returns>
		public bool Authorize(DashboardContext context)
		{
			// In case you need an OWIN context, use the next line, `OwinContext` class
			// is the part of the `Microsoft.Owin` package.
			var owinContext = new OwinContext(context.GetOwinEnvironment());

			// Allow all authenticated users to see the Dashboard (potentially dangerous).
			return owinContext.Authentication.User.Identity.IsAuthenticated || IsLocalhost(context);
		}

		/// <summary>
		/// Determines whether the specified context is localhost.
		/// </summary>
		/// <param name="context">The context.</param>
		/// <returns>
		///   <c>true</c> if the specified context is localhost; otherwise, <c>false</c>.
		/// </returns>
		public bool IsLocalhost(DashboardContext context)
		{
			return !string.IsNullOrEmpty(context.Request.RemoteIpAddress) && (context.Request.RemoteIpAddress == "127.0.0.1" || context.Request.RemoteIpAddress == "::1" || context.Request.RemoteIpAddress == context.Request.LocalIpAddress);
		}
	}
}