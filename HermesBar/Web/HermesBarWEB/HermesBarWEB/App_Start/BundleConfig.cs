using System.Web;
using System.Web.Optimization;

namespace HermesBarWEB
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            #region Scripts
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include("~/Scripts/jquery.js"));
            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include("~/Scripts/bootstrap.js"));
            bundles.Add(new ScriptBundle("~/bundles/sweetalert").Include("~/Scripts/sweetalert-dev.js"));
            bundles.Add(new ScriptBundle("~/bundles/hma").Include("~/Scripts/HMA.js"));
            bundles.Add(new ScriptBundle("~/bundles/scrollTo").Include("~/Scripts/jquery.scrollTo.min.js"));
            bundles.Add(new ScriptBundle("~/bundles/niceScroll").Include("~/Scripts/jquery.nicescroll.js"));
            bundles.Add(new ScriptBundle("~/bundles/jqueryMask").Include("~/Scripts/jquery-mask.js"));
            bundles.Add(new ScriptBundle("~/bundles/scripts").Include("~/Scripts/scripts.js"));
            bundles.Add(new ScriptBundle("~/bundles/calendar").Include("~/Scripts/fullcalendar.js"));
            bundles.Add(new ScriptBundle("~/bundles/moment").Include("~/Scripts/moment.min.js"));
            #endregion

            #region Styles
            bundles.Add(new StyleBundle("~/content/bootstrap").Include("~/Content/bootstrap.css"));
            bundles.Add(new StyleBundle("~/content/bootstrap-theme").Include("~/Content/bootstrap-theme.css"));
            bundles.Add(new StyleBundle("~/content/elegante-icons").Include("~/Content/elegant-icons-style.css"));
            bundles.Add(new StyleBundle("~/content/font-awesome").Include("~/Content/font-awesome.min.css"));
            bundles.Add(new StyleBundle("~/content/style").Include("~/Content/style.css"));
            bundles.Add(new StyleBundle("~/content/responsive").Include("~/Content/style-responsive.css"));
            bundles.Add(new StyleBundle("~/content/sweetalert").Include("~/Content/sweetalert.css"));
            bundles.Add(new StyleBundle("~/content/calendar").Include("~/Content/fullcalendar.css"));
            #endregion
        }
    }
}