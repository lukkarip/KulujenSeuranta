using System.Web;
using System.Web.Optimization;

namespace KulujenSeuranta
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryvalUnobtrusive").Include(
                        "~/Scripts/jquery.validate.unobtrusive.js"));

            bundles.Add(new ScriptBundle("~/bundles/globalize").Include(
                        "~/Scripts/globalize/globalize.js"));

            bundles.Add(new ScriptBundle("~/bundles/cultureFi").Include(
                        "~/Scripts/globalize/cultures/globalize.culture.fi-FI.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryvalGlobalize").Include(
                        "~/Scripts/jquery.validate.globalize.js"));

            bundles.Add(new ScriptBundle("~/bundles/kulttuuri").Include(
                       "~/Scripts/kulttuuri.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrapSelect").Include(
                       "~/Scripts/bootstrap-select.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/bootstrap-datepicker.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/datepicker.css",
                      "~/Content/datepicker3.css",
                      "~/Content/site.css",
                      "~/Content/bootstrap-select.css"));

            // Set EnableOptimizations to false for debugging. For more information,
            // visit http://go.microsoft.com/fwlink/?LinkId=301862
            BundleTable.EnableOptimizations = true;
        }
    }
}
