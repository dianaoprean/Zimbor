using System.Web;
using System.Web.Optimization;

namespace UI.Admin
{
	public class BundleConfig
	{
		// For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
		public static void RegisterBundles(BundleCollection bundles)
		{
			bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
						"~/Scripts/lib/jquery-{version}.js"));

			bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
						"~/Scripts/lib/jquery-ui-{version}.js"));
            bundles.Add(new ScriptBundle("~/bundles/jquery.unobtrusive-ajax").Include("~/Scripts/lib/jquery.unobtrusive-ajax.js"));


            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
              "~/Scripts/lib/jquery.validate*"));

			// Use the development version of Modernizr to develop with and learn from. Then, when you're
			// ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
			bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
						"~/Scripts/lib/modernizr-*"));

			bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include("~/Scripts/lib/bootstrap.js"));

			bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/site.css",
				"~/Content/font-awesome-4.3.0/css/font-awesome.css",
					"~/Content/bootstrap.css",
                    "~/Content/bootstrap-dialog.css",
						"~/Content/AdminLTE.css",
						"~/Content/_all-skins.css"
				));

			bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
						"~/Content/themes/base/jquery.ui.core.css",
						"~/Content/themes/base/jquery.ui.resizable.css",
						"~/Content/themes/base/jquery.ui.selectable.css",
						"~/Content/themes/base/jquery.ui.accordion.css",
						"~/Content/themes/base/jquery.ui.autocomplete.css",
						"~/Content/themes/base/jquery.ui.button.css",
						"~/Content/themes/base/jquery.ui.dialog.css",
						"~/Content/themes/base/jquery.ui.slider.css",
						"~/Content/themes/base/jquery.ui.tabs.css",
						"~/Content/themes/base/jquery.ui.datepicker.css",
						"~/Content/themes/base/jquery.ui.progressbar.css",
						"~/Content/themes/base/jquery.ui.theme.css"
					
						));

            bundles.Add(new StyleBundle("~/Content/ui").Include("~/Content/themes/base/jquery-ui.css"));

            bundles.Add(new StyleBundle("~/Content/jqGridCss").Include("~/Content/ui.jqgrid.css", "~/Content/ui.jqgrid-bootstarp.css"));

            bundles.Add(new ScriptBundle("~/bundles/jqGrid").Include("~/Scripts/lib/jqGrid/js/i18n/grid.locale-en.js",
                                                                     "~/Scripts/lib/jqGrid/js/jquery.jqGrid.src.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap-dialog").Include(
                              "~/Scripts/lib/bootstrap-dialog.js"));

            // File Upload Plugin
            bundles.Add(new StyleBundle("~/Scripts/lib/fileUpload/css").Include(
                    "~/Scripts/lib/fileUpload/css/bootstrap.css",
                    "~/Scripts/lib/fileUpload/css/bootstrap-responsive.css",
                    "~/Scripts/lib/fileUpload/css/jquery.fileupload.css",
                    "~/Scripts/lib/fileUpload/css/bootstrap-image-gallery.css",
                    "~/Scripts/lib/fileUpload/css/jquery.fileupload-ui.css"));

            bundles.Add(new ScriptBundle("~/bundles/fileUpload").Include(
                    "~/Scripts/lib/fileUpload/js/vendor/jquery.ui.widget.js",
                    "~/Scripts/lib/fileUpload/js/helpers/tmpl.js",
                    "~/Scripts/lib/fileUpload/js/helpers/load-image.js",
                    "~/Scripts/lib/fileUpload/js/helpers/canvas-to-blob.js",
                    "~/Scripts/lib/fileUpload/js/helpers/bootstrap.js",
                    "~/Scripts/lib/fileUpload/js/helpers/bootstrap-image-gallery.js",
                    "~/Scripts/lib/fileUpload/js/jquery.iframe-transport.js",
                    "~/Scripts/lib/fileUpload/js/jquery.fileupload.js",
                    "~/Scripts/lib/fileUpload/js/jquery.fileupload-process.js",
                    "~/Scripts/lib/fileUpload/js/jquery.fileupload-image.js",
                    "~/Scripts/lib/fileUpload/js/jquery.fileupload-audio.js",
                    "~/Scripts/lib/fileUpload/js/jquery.fileupload-video.js",
                    "~/Scripts/lib/fileUpload/js/jquery.fileupload-validate.js",
                    "~/Scripts/lib/fileUpload/js/jquery.fileupload-ui.js",
                    "~/Scripts/lib/fileUpload/fileupload.js",
                    "~/Scripts/lib/fileUpload/js/cors/jquery.xdr-transport.js"
                ));


            bundles.Add(new ScriptBundle("~/bundles/common").Include("~/Scripts/common.js", "~/Scripts/app.js", "~/Scripts/api.js"));

            bundles.Add(new ScriptBundle("~/bundles/campanii-marketing").Include("~/Scripts/campanii-marketing.js"));

            bundles.Add(new ScriptBundle("~/bundles/clienti").Include("~/Scripts/clienti.js"));

            bundles.Add(new ScriptBundle("~/bundles/cms").Include("~/Scripts/cms.js"));

            bundles.Add(new ScriptBundle("~/bundles/concurenta").Include("~/Scripts/concurenta.js"));

            bundles.Add(new ScriptBundle("~/bundles/primire-turistica").Include("~/Scripts/primire-turistica.js"));

            bundles.Add(new ScriptBundle("~/bundles/produse").Include("~/Scripts/produse.js"));

            bundles.Add(new ScriptBundle("~/bundles/studiu-piata").Include("~/Scripts/studiu-piata.js"));

            bundles.Add(new ScriptBundle("~/bundles/user-management").Include("~/Scripts/user-management.js"));
		}
	}
}