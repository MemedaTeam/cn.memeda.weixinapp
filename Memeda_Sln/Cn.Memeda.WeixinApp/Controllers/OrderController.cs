using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cn.Memeda.WeixinApp.Controllers
{
    public class OrderController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.ShowBack = true;
            return View();
        }

        public ActionResult Info()
        {
            ViewBag.ShowBack = true;
            return View();
        }

        public ActionResult Pay()
        {
            ViewBag.ShowBack = true;
            return View();
        }

    }
}