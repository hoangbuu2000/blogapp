using System.Web;
using System.Web.Optimization;

namespace BlogApp
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/js").Include(
                        "~/Scripts/custom.js",
                        "~/Scripts/tether.min.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/style.css",
                      "~/Content/tech.css",
                      "~/Content/responsive.css",
                      "~/Content/font-awesome.min.css",
                      "~/Content/colors.css"));

            bundles.Add(new StyleBundle("~/AdminContent/css").Include(
                      "~/Areas/Admin/assets/css/material-dashboard.min.css", 
                      "~/Areas/Admin/assets/demo/demo.css"));

            bundles.Add(new ScriptBundle("~/Adminbundles/jquery").Include(
                      "~/Areas/Admin/assets/js/core/jquery.min.js",
                      "~/Areas/Admin/assets/js/core/popper.min.js",
                      "~/Areas/Admin/assets/js/plugins/perfect-scrollbar.jquery.min.js",
                      "~/Areas/Admin/assets/js/plugins/moment.min.js",
                      "~/Areas/Admin/assets/js/plugins/sweetalert2.js",
                      "~/Areas/Admin/assets/js/plugins/jquery.validate.min.js",
                      "~/Areas/Admin/assets/js/plugins/jquery.bootstrap-wizard.js",
                      "~/Areas/Admin/assets/js/plugins/bootstrap-datetimepicker.min.js",
                      "~/Areas/Admin/assets/js/plugins/jquery.datatables.min.js",
                      "~/Areas/Admin/assets/js/plugins/bootstrap-tagsinput.js",
                      "~/Areas/Admin/assets/js/plugins/jasny-bootstrap.min.js",
                      "~/Areas/Admin/assets/js/plugins/fullcalendar.min.js",
                      "~/Areas/Admin/assets/js/plugins/jquery-jvectormap.js",
                      "~/Areas/Admin/assets/js/plugins/nouislider.min.js",
                      "~/Areas/Admin/assets/js/plugins/arrive.min.js",
                      "~/Areas/Admin/assets/js/plugins/chartist.min.js",
                      "~/Areas/Admin/assets/js/material-dashboard.js",
                      "~/Areas/Admin/assets/demo/demo.js",
                      "~/Scripts/jquery.unobtrusive-ajax.min.js"));

            bundles.Add(new ScriptBundle("~/Adminbundles/bootstrap").Include(
                      "~/Areas/Admin/assets/js/core/bootstrap-material-design.min.js",
                      "~/Areas/Admin/assets/js/plugins/bootstrap-notify.js"));

            bundles.Add(new StyleBundle("~/Content/login/css").Include(
                      "~/Content/login/vendor/bootstrap/css/bootstrap.min.css",
                      "~/Content/login/fonts/font-awesome-4.7.0/css/font-awesome.min.css",
                      "~/Content/login/fonts/iconic/css/material-design-iconic-font.min.css",
                      "~/Content/login/vendor/animate/animate.css",
                      "~/Content/login/vendor/css-hamburgers/hamburgers.min.css",
                      "~/Content/login/vendor/animsition/css/animsition.min.css",
                      "~/Content/login/vendor/select2/select2.min.css",
                      "~/Content/login/vendor/daterangepicker/daterangepicker.css",
                      "~/Content/login/css/util.css",
                      "~/Content/login/css/main.css"));

            bundles.Add(new ScriptBundle("~/Content/login/js").Include(
                      "~/Content/login/vendor/jquery/jquery-3.2.1.min.js",
                      "~/Content/login/vendor/animsition/js/animsition.min.js",
                      "~/Content/login/vendor/bootstrap/js/popper.js",
                      "~/Content/login/vendor/bootstrap/js/bootstrap.min.js",
                      "~/Content/login/vendor/select2/select2.min.js",
                      "~/Content/login/vendor/daterangepicker/moment.min.js",
                      "~/Content/login/vendor/daterangepicker/daterangepicker.js",
                      "~/Content/login/vendor/countdowntime/countdowntime.js",
                      "~/Content/login/js/main.js"));

            bundles.Add(new ScriptBundle("~/Vendor/Ckeditor").Include(
                      "~/Content/ckeditor/ckeditor.js",
                      "~/Content/ckfinder/ckfinder.js"));
        }
    }
}
