using System.Web;
using System.Web.Optimization;

namespace SGEA
{
    public class BundleConfig
    {
        // Para obtener más información sobre las uniones, visite https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Content/vendors/jquery/dist/jquery.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Utilice la versión de desarrollo de Modernizr para desarrollar y obtener información. De este modo, estará
            // para la producción, use la herramienta de compilación disponible en https://modernizr.com para seleccionar solo las pruebas que necesite.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Content/vendors/bootstrap/dist/js/bootstrap.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/gentelella").Include(
                        "~/Content/vendors/fastclick/lib/fastclick.js",
                        "~/Content/vendors/nprogress/nprogress.js",
                        "~/Content/vendors/Chart.js/dist/Chart.min.js",
                        "~/Content/vendors/gauge.js/dist/gauge.min.js",
                        "~/Content/vendors/bootstrap-progressbar/bootstrap-progressbar.min.js",
                        "~/Content/vendors/iCheck/icheck.min.js",
                        "~/Content/vendors/skycons/skycons.js",
                        "~/Content/vendors/Flot/jquery.flot.js",
                        "~/Content/vendors/Flot/jquery.flot.pie.js",
                        "~/Content/vendors/Flot/jquery.flot.time.js",
                        "~/Content/vendors/Flot/jquery.flot.stack.js",
                        "~/Content/vendors/Flot/jquery.flot.resize.js",
                        "~/Content/vendors/flot.orderbars/js/jquery.flot.orderBars.js",
                        "~/Content/vendors/flot-spline/js/jquery.flot.spline.min.js",
                        "~/Content/vendors/flot.curvedlines/curvedLines.js",
                        "~/Content/vendors/DateJS/build/date.js",
                        "~/Content/vendors/jqvmap/dist/jquery.vmap.js",
                        "~/Content/vendors/jqvmap/dist/maps/jquery.vmap.world.js",
                        "~/Content/vendors/jqvmap/examples/js/jquery.vmap.sampledata.js",
                        "~/Content/vendors/moment/min/moment.min.js",
                        "~/Content/vendors/bootstrap-daterangepicker/daterangepicker.js",
                        "~/Content/vendors/bootstrap-datetimepicker/build/js/bootstrap-datetimepicker.min.js",
                        "~/Content/build/js/custom.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/forms").Include(
                      "~/Content/vendors/bootstrap-wysiwyg/js/bootstrap-wysiwyg.min.js",
                      "~/Content/vendors/jquery.hotkeys/jquery.hotkeys.js",
                      "~/Content/vendors/google-code-prettify/src/prettify.js",
                      "~/Content/vendors/jquery.tagsinput/src/jquery.tagsinput.js",
                      "~/Content/vendors/switchery/dist/switchery.min.js",
                      "~/Content/vendors/select2/dist/js/select2.full.min.js",
                      "~/Content/vendors/parsleyjs/dist/parsley.min.js",
                      "~/Content/vendors/autosize/dist/autosize.min.js",
                      "~/Content/vendors/devbridge-autocomplete/dist/jquery.autocomplete.min.js",
                      "~/Content/vendors/starrr/dist/starrr.js"));

            bundles.Add(new ScriptBundle("~/bundles/datatable").Include(
                        "~/Content/vendors/datatables.net/js/jquery.dataTables.min.js",
                        "~/Content/vendors/datatables.net-bs/js/dataTables.bootstrap.min.js",
                        "~/Content/vendors/datatables.net-buttons/js/dataTables.buttons.min.js",
                        "~/Content/vendors/datatables.net-buttons-bs/js/buttons.bootstrap.min.js",
                        "~/Content/vendors/datatables.net-buttons/js/buttons.flash.min.js", 
                        "~/Content/vendors/datatables.net-buttons/js/buttons.html5.min.js",
                        "~/Content/vendors/datatables.net-buttons/js/buttons.print.min.js",
                        "~/Content/vendors/datatables.net-fixedheader/js/dataTables.fixedHeader.min.js",
                        "~/Content/vendors/datatables.net-keytable/js/dataTables.keyTable.min.js",
                        "~/Content/vendors/datatables.net-responsive/js/dataTables.responsive.min.js",
                        "~/Content/vendors/datatables.net-responsive-bs/js/responsive.bootstrap.js",
                        "~/Content/vendors/datatables.net-scroller/js/dataTables.scroller.min.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/vendors/bootstrap/dist/css/bootstrap.min.css"));
            //"~/Content/site.css",

            bundles.Add(new StyleBundle("~/Content/forms").Include(
                     "~/Content/vendors/select2/dist/css/select2.min.css"));

            bundles.Add(new StyleBundle("~/Datatable/css").Include(
                      "~/Content/vendors/datatables.net-bs/css/dataTables.bootstrap.min.css",
                      "~/Content/vendors/datatables.net-buttons-bs/css/buttons.bootstrap.min.css",
                      "~/Content/vendors/datatables.net-fixedheader-bs/css/fixedHeader.bootstrap.min.css",
                      "~/Content/vendors/datatables.net-responsive-bs/css/responsive.bootstrap.min.css",
                      "~/Content/vendors/datatables.net-scroller-bs/css/scroller.bootstrap.min.css"));

            bundles.Add(new StyleBundle("~/Gentelella/css").Include(
                      "~/Content/vendors/font-awesome/css/font-awesome.min.css",
                      "~/Content/vendors/nprogress/nprogress.css",
                      "~/Content/vendors/bootstrap-progressbar/css/bootstrap-progressbar-3.3.4.min.css",
                      "~/Content/vendors/iCheck/skins/flat/green.css",
                      "~/Content/vendors/jqvmap/dist/jqvmap.min.css",
                      "~/Content/vendors/bootstrap-daterangepicker/daterangepicker.css",
                      "~/Content/vendors/bootstrap-datetimepicker/build/css/bootstrap-datetimepicker.min.css",
                      "~/Content/build/css/custom.min.css"));
        }
    }
}
