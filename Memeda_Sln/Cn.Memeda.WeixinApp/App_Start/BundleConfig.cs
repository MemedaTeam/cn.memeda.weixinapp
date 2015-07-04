﻿using System.Web;
using System.Web.Optimization;

namespace Cn.Memeda.WeixinApp
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/common.js",
                      "~/Scripts/respond.js"));
            bundles.Add(new ScriptBundle("~/bundles/account").Include(
                     "~/Scripts/account/login.js"));
            bundles.Add(new ScriptBundle("~/bundles/common").Include(
                     "~/Scripts/common.js"));

            bundles.Add(new ScriptBundle("~/bundles/shop").Include(
                    "~/Scripts/shop/car.js",
                     "~/Scripts/shop/shop.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/public.css",
                      "~/content/common.css",
                      "~/Content/site.css"));
        }
    }
}
