using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.UI.WebControls;

namespace culinaryConnect.Web.App_Start
{
	public static class BundleConfig
	{
		public static void RegisterBundle(BundleCollection bundles)
		{
			bundles.Add(new StyleBundle("~/bundles/style/css").
				Include("~/Content/Styles/style.css", new CssRewriteUrlTransform()));

            bundles.Add(new StyleBundle("~/bundles/bootstrap/css").Include(
                "~/Content/Styles/bootstrap.min.css", new CssRewriteUrlTransform()));

            bundles.Add(new StyleBundle("~/bundles/fontawesome/css").Include(
                "~/Content/Styles/font-awesome.min.css", new CssRewriteUrlTransform()));

            bundles.Add(new StyleBundle("~/bundles/niceselect/css").Include(
                "~/Content/Styles/nice-select.css", new CssRewriteUrlTransform()));

            bundles.Add(new StyleBundle("~/bundles/slicknav/css").Include(
                "~/Content/Styles/slicknav.min.css", new CssRewriteUrlTransform()));

            bundles.Add(new StyleBundle("~/bundles/style/css").Include(
                "~/Content/Styles/style.css"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap/js").
                Include("~/Content/Scripts/bootstrap.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery3.3.1/js").
                Include("~/Content/Scripts/jquery-3.3.1.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/niceselect/js").
                Include("~/Content/Scripts/jquery.nice-select.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/slicknav/js").
                Include("~/Content/Scripts/jquery.slicknav.js"));

            bundles.Add(new ScriptBundle("~/bundles/main/js").
                Include("~/Content/Scripts/main.js"));

            bundles.Add(new ScriptBundle("~/bundles/mixitup/js").
                Include("~/Content/Scripts/mixitup.min.js"));






        }
    }
}