using System.Web.Optimization;
using System.Web.Optimization.React;
using StockTradingAnalysis.Interfaces.Common;

namespace StockTradingAnalysis.Web.BootModules
{
    public class BundlesBootModule : IBootModule
    {
        public int Priority => 0;

        public void Boot()
        {
            RegisterBundles(BundleTable.Bundles);
        }

        private static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/bundles/css").Include(
                "~/Content/Css/bootstrap.css", //https://bootswatch.com/slate/
                "~/Content/Css/site.css",
                "~/Content/Css/font-awesome.min.css",
                "~/Content/Css/bootstrap-datetimepicker.min.css"));

            bundles.Add(new ScriptBundle("~/bundles/js").Include(
                "~/Content/Js/custom.js",
                "~/Content/Js/bootstrap.js",
                "~/Content/Js/moment-with-locales.js",
                "~/Content/Js/bootstrap-datetimepicker.min.js",
                "~/Content/Js/axios.min.js")); //https://github.com/axios/axios

            bundles.Add(new ScriptBundle("~/bundles/validation").Include(
                "~/Scripts/jquery.validate.min.js",
                "~/Scripts/jquery.validate.unobtrusive.min.js",
                "~/Scripts/validate.decimal.fix.js"));

            bundles.Add(new ScriptBundle("~/bundles/react").Include(
                "~/Content/Js/react.js",
                "~/Content/Js/react-dom.js"));

            bundles.Add(new BabelBundle("~/bundles/jsx").Include(
                "~/Content/Jsx/Popup.jsx",
                "~/Content/Jsx/UpdateQuotationsButton.jsx",
                "~/Content/Jsx/GenerateTestDataButtonProgressBar.jsx"));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/jquery-{version}.js",
                "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/signalr").Include(
                "~/Scripts/jquery.signalR-{version}.js"));

            BundleTable.EnableOptimizations = false;
        }
    }
}