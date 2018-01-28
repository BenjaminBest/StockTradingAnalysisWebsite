using System.Web.Optimization;
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
                "~/Content/Css/bootstrap.css", //-->https://bootswatch.com/slate/
                "~/Content/Css/site.css",
                "~/Content/Css/font-awesome.min.css",
                "~/Content/Css/bootstrap-datetimepicker.min.css"));

            bundles.Add(new ScriptBundle("~/bundles/js").Include(
                "~/Content/Js/custom.js",
                "~/Content/Js/bootstrap.js",
                "~/Content/Js/moment-with-locales.js",
                "~/Content/Js/bootstrap-datetimepicker.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/validation").Include(
                "~/Scripts/jquery.validate.min.js",
                "~/Scripts/jquery.validate.unobtrusive.min.js",
                "~/Scripts/validate.decimal.fix.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                "~/Scripts/jquery-ui-{version}.js"));

            BundleTable.EnableOptimizations = false;
        }
    }
}