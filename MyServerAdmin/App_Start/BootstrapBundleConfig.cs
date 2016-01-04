using System.Web.Optimization;

[assembly: WebActivatorEx.PostApplicationStartMethod(typeof(MyServerAdmin.App_Start.BootstrapBundleConfig), "RegisterBundles")]

namespace MyServerAdmin.App_Start
{
	public class BootstrapBundleConfig
	{
		public static void RegisterBundles()
		{
			BundleTable.Bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include("~/Scripts/bootstrap.js"));
			BundleTable.Bundles.Add(new StyleBundle("~/Content/bootstrap").Include("~/Content/bootstrap.css"));
			BundleTable.Bundles.Add(new StyleBundle("~/Content/bootstrap-theme").Include("~/Content/bootstrap-theme.css"));
		}
	}
}
