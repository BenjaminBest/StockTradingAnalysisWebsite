using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;

namespace StockTradingAnalysis.Web.Common.Extensions
{
	/// <summary>
	/// Link extensions contains HTML helpers for generating links.
	/// </summary>
	public static class LinkExtensions
	{
		/// <summary>
		/// Returns a plain link.
		/// </summary>
		/// <param name="htmlHelper">The HTML helper.</param>
		/// <param name="action">The action.</param>
		/// <param name="controller">The controller.</param>
		/// <param name="parameters">The parameters.</param>
		/// <returns></returns>
		public static IHtmlContent PlainLink(this IHtmlHelper htmlHelper, string action, string controller,
			object parameters)
		{
			var helper = new UrlHelper(htmlHelper.ViewContext);
			var url = helper.Action(action, controller, parameters);

			return new HtmlString(url);
		}
	}
}
