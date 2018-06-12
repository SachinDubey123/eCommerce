using System.Web.Optimization;

namespace UserInterface.eCommerce
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/js").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/bootstrap.min.js"));
            bundles.Add(new StyleBundle("~/Content/css").Include(
                      
                       "~/Content/Customer/css/bootstrap.css"
                      ));

            bundles.Add(new ScriptBundle("~/bundles/Adminjs").Include(
                    "~/Scripts/js/bootstrap.js",
                        "~/Scripts/fastclick.js",
                        "~/Scripts/icheck.min.js",
                        "~/Scripts/adminlte.min.js",
                        "~/Scripts/jquery.sparkline.min.js",
                        "~/Scripts/jquery-jvectormap-1.2.2.min.js",
                        "~/Scripts/jquery-jvectormap-world-mill-en.js",
                        "~/Scripts/jquery.slimscroll.min.js",
                        "~/Scripts/Chart.js"
                        ));
            bundles.Add(new StyleBundle("~/Content/Admincss").Include(
                     
                      "~/Content/jquery-jvectormap.css",
                      "~/Content/Model.css",
                      "~/Content/_all-skins.min.css",
                      "~/Content/font-awesome.css",
                      "~/Content/ionicons.min.css",
                      "~/Content/AdminLTE.min.css",
                      "~/Content/blue.css",
                      "~/Content/fonts.googleapis.com.css"
                      ));

            bundles.Add(new StyleBundle("~/Content/DataTablecss").Include(
                       "~/Content/jquery-ui.min.css",
                       "~/Content/jquery.dataTables.min.css",
                       "~/Content/dataTables.bootstrap.min.css",
                       "~/Content/responsive.bootstrap.min.css"
                       ));

            bundles.Add(new ScriptBundle("~/bundles/DataTablejs").Include(
                      "~/Scripts/jquery.dataTables.min.js",
                      "~/Scripts/jquery-ui.min.js",
                      "~/Scripts/dataTables.bootstrap.min.js",
                      "~/Scripts/dataTables.responsive.min.js",
                      "~/Scripts/responsive.bootstrap.min.js"
                      ));

            bundles.Add(new StyleBundle("~/Content/Customerboostrap/Customerboostrap").Include(
               "~/Content/Customerboostrap/bootstrap.css"
               ));

            bundles.Add(new StyleBundle("~/Content/Customercss").Include(
                "~/Content/Customer/css/bootstrap.css",
                "~/Content/Model.css",
                 "~/Content/Customer/css/flexslider.css",
                "~/Content/Customer/css/style.css",
                "~/Content/family=Roboto.css",
                "~/Content/Customer/css/font-awesome.css",
                "~/Content/Customer/css/easy-responsive-tabs.css",
                "~/Content/family=OpenSans300300i400400i600600i700700i800.css",
                "~/Content/family=Lato400100100italic30/0300italic400italic700900900italic700italic.css"
                
                 ));

           

           bundles.Add(new ScriptBundle("~/bundles/Customerjs").Include(
                 "~/Scripts/js/jquery-2.1.4.min.js",
                 "~/Scripts/apis.google.com-js-api-client.js",
                  "~/Scripts/js/modernizr.custom.js",
                  "~/Scripts/apis.google.com-js-api-client.js",
                   "~/Scripts/js/Cart.js",
                    "~/Scripts/js/easy-responsive-tabs.js",
                           "~/Scripts/js/jquery.waypoints.min.js",
                    "~/Scripts/js/jquery.countup.js",

                    "~/Scripts/js/move-top.js",

                    "~/Scripts/js/jquery.easing.min.js",
                    "~/Scripts/js/bootstrap.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/Admin.js").Include(
                "~/Content/Admin.js"
                ));

            bundles.Add(new ScriptBundle("~/Scripts/js/Product.js").Include(
                "~/Scripts/js/Product.js"
                ));

            bundles.Add(new ScriptBundle("~/Scripts/Category.js").Include(
                "~/Scripts/Category.js"
                ));

            bundles.Add(new ScriptBundle("~/Scripts/Sweet.js").Include(
              "~/Scripts/sweet-alert.js"
              ));

            bundles.Add(new StyleBundle("~/Content/Sweet.css").Include(
                            "~/Content/sweet-alert.css"
                      ));
            
        }
    }
}