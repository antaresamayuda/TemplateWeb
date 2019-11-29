using System.Web;
using System.Web.Optimization;

namespace TemplateWeb.MVC
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-3.2.1.js")
                        );

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-1.8.24.js",
                        "~/Scripts/jquery.blockUI.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate.js",
                        "~/Scripts/jquery.validate.unobtrusive.js"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/Script/DataTable").Include(
                        "~/Scripts/datatable/complete.min.js",
                        "~/Scripts/datatable/FixedColumns.js",
                        "~/Scripts/datatable/jquery.dataTables.js",                
                        "~/Scripts/DataTables/dataTables.bootstrap.js"
                        ));

            bundles.Add(new StyleBundle("~/Content/DataTable").Include(       
                        "~/Content/DataTables/css/jquery.dataTables.min.css",
                        "~/Content/DataTables/css/dataTables.bootstrap.css",
                        "~/Content/bootstrap.min.css"
                        ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                        "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/plugins/css").Include(
                       "~/Content/plugins/select2/select2.css"
                       ));

            bundles.Add(new ScriptBundle("~/Content/plugins").Include(
                "~/Content/plugins/select2/select2.full.js",
                "~/Content/plugins/input-mask/jquery.inputmask.js",
                "~/Content/plugins/input-mask/jquery.inputmask.date.extensions.js",
                "~/Content/plugins/input-mask/jquery.inputmask.extensions.js",
                "~/Content/plugins/datepicker/bootstrap-datepicker.js",
                "~/Content/plugins/timepicker/bootstrap-timepicker.js",
                "~/Content/plugins/slimScroll/jquery.slimscroll.js",
                "~/Content/plugins/iCheck/icheck.js",
                "~/Content/plugins/fastclick/fastclick.js"
                ));

            bundles.Add(new ScriptBundle("~/Scripts/fancybox").Include(
                    "~/Scripts/fancybox/jquery.fancybox.pack.js"
                ));

            bundles.Add(new StyleBundle("~/Content/fancybox").Include(
                    "~/Content/fancybox/jquery.fancybox.css"
                ));

            bundles.Add(new ScriptBundle("~/Scripts/bootstrap").Include(
                    "~/Scripts/bootstrap.js",
                    "~/Scripts/bootstrap-multiselect.js"
                ));

            bundles.Add(new StyleBundle("~/Content/bootstrap").Include(
                    "~/Content/bootstrap/css/bootstrap.css"                  
                    ));

            bundles.Add(new StyleBundle("~/Script/Multiselect").Include(
                    "~/Content/bootstrap-multiselect.css"
                    ));

            System.Web.Optimization.BundleTable.EnableOptimizations = false;
        }
    }
}
