using Hangfire.Dashboard;

namespace StockTradingAnalysis.Web.Common.Authorization
{
	/// <summary>
	/// The HangfireAuthorizationFilter authorizes access from localhost or authenticated users.
	/// </summary>
	/// <seealso cref="Hangfire.Dashboard.IDashboardAuthorizationFilter" />
	public class HangfireAuthorizationFilter : IDashboardAuthorizationFilter
	{
		/// <summary>
		/// Authorizes the specified context.
		/// </summary>
		/// <param name="context">The context.</param>
		/// <returns></returns>
		public bool Authorize(DashboardContext context)
		{
			var httpContext = context.GetHttpContext();

			// Allow all authenticated users to see the Dashboard (potentially dangerous).
			return httpContext.User.Identity.IsAuthenticated || IsLocalhost(context);
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