using System.Web.Mvc;
using System.Web.Routing;

namespace StockTradingAnalysis.Web.Common.Extensions
{
    /// <summary>
    /// Html Externsion used by the razor views
    /// </summary>
    public static class HtmlExtensions
    {
        //TODO: Check if needed
        /// <summary>
        /// Returns the dislay attribute of the data annotations for the given model and property
        /// </summary>
        /// <typeparam name="TModel">Model</typeparam>
        /// <typeparam name="TProperty">Property</typeparam>
        /// <param name="htmlHelper">Html Helper</param>
        /// <param name="expression">Expression</param>
        /// <returns>Display Attribute</returns>
        //public static MvcHtmlString GetDisplayName<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper,
        //    Expression<Func<TModel, TProperty>> expression)
        //{
        //    var metaData = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
        //    var value = metaData.DisplayName ??
        //                (metaData.PropertyName ?? ExpressionHelper.GetExpressionText(expression));
        //    return MvcHtmlString.Create(value);
        //}

        //TODO: Check if needed
        /// <summary>
        /// A Simple ActionLink Image
        /// </summary>
        /// <param name="actionName">name of the action in controller</param>
        /// <param name="imgUrl">url of the image</param>
        /// <param name="controller">Controller</param>
        /// <param name="alt">alt text of the image</param>
        /// <returns></returns>
        //public static MvcHtmlString ImageLink(this HtmlHelper helper, string actionName, string controller,
        //    string imgUrl, string alt)
        //{
        //    return ImageLink(helper, actionName, controller, imgUrl, alt, null, null, null);
        //}

        //TODO: Check if needed
        /// <summary>
        /// A Simple ActionLink Image
        /// </summary>
        /// <param name="actionName">name of the action in controller</param>
        /// <param name="imgUrl">url of the iamge</param>
        /// <param name="controller">Controller</param>
        /// <param name="alt">alt text of the image</param>
        /// <returns></returns>
        //public static MvcHtmlString ImageLink(this HtmlHelper helper, string actionName, string controller,
        //    string imgUrl, string alt, object routeValues)
        //{
        //    return ImageLink(helper, actionName, controller, imgUrl, alt, routeValues, null, null);
        //}

        /// <summary>
        /// Renders a a simple action-link for an image
        /// </summary>
        /// <param name="actionName">name of the action in controller</param>
        /// <param name="imgUrl">url of the image</param>
        /// <param name="alt">alt text of the image</param>
        /// <param name="linkHtmlAttributes">attributes for the link</param>
        /// <param name="imageHtmlAttributes">attributes for the image</param>
        /// <param name="controller">Controller</param>
        /// <param name="routeValues">Route values</param>
        /// <returns></returns>
        public static MvcHtmlString ImageLink(this HtmlHelper helper, string actionName, string controller,
            string imgUrl, string alt, object routeValues, object linkHtmlAttributes, object imageHtmlAttributes)
        {
            var urlHelper = new UrlHelper(helper.ViewContext.RequestContext);
            var url = urlHelper.Action(actionName, controller, routeValues);

            //Create the link
            var linkTagBuilder = new TagBuilder("a");
            linkTagBuilder.MergeAttribute("href", url);
            linkTagBuilder.MergeAttributes(new RouteValueDictionary(linkHtmlAttributes));

            //Create image
            var imageTagBuilder = new TagBuilder("img");

            if (!string.IsNullOrEmpty(imgUrl))
            {
                imageTagBuilder.MergeAttribute("src", urlHelper.Content(imgUrl));
            }

            imageTagBuilder.MergeAttribute("alt", urlHelper.Content(alt));
            imageTagBuilder.MergeAttributes(new RouteValueDictionary(imageHtmlAttributes));

            //Add image to link
            linkTagBuilder.InnerHtml = imageTagBuilder.ToString(TagRenderMode.SelfClosing);

            return MvcHtmlString.Create(linkTagBuilder.ToString());
        }

        /// <summary>
        /// Renders a simple link without the href.
        /// </summary>
        /// <param name="helper">helper</param>
        /// <param name="actionName">Name of the action in controller</param>
        /// <param name="controller">Controller</param>
        /// <param name="routeValues">Route values</param>
        public static MvcHtmlString PlainLink(this HtmlHelper helper, string actionName, string controller,
            object routeValues)
        {
            var urlHelper = new UrlHelper(helper.ViewContext.RequestContext);
            var url = urlHelper.Action(actionName, controller, routeValues);

            return MvcHtmlString.Create(url);
        }
    }
}