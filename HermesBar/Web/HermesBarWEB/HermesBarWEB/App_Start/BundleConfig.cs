using System.Web;
using System.Web.Optimization;

namespace HermesBarWEB
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            #region Scripts
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include("~/Scripts/jquery-{version}.js"));
            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include("~/Scripts/bootstrap.js"));
            #endregion

            #region Styles
            bundles.Add(new StyleBundle("~/content/bootstrap").Include("~/Content/bootstrap.css"));
            #endregion
        }
    }
}