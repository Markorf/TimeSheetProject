using System.Web;
using System.Web.Optimization;

namespace TimeSheet.PL.WebApplication
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/main/jquery-1.8.3.min.js"));


            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/main/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/main").Include(
                         "~/Scripts/main/default.js",
                        "~/Scripts/main/accordion.js",
                      "~/Scripts/libs/jquery.fancybox.js"));

            bundles.Add(new ScriptBundle("~/bundles/ui").Include(
                      "~/Scripts/libs/jquery-ui-1.9.2.custom.min.js",
                      "~/Scripts/main/default.js"));

            bundles.Add(new StyleBundle("~/Content/style").Include(
                 "~/Content/style.css"
                )
               );
        }
    }
}
