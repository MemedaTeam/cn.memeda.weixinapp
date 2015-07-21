using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cn.Memeda.WeixinApp.Controllers
{
    public class ShopController : Controller
    {
        //
        // GET: /Shop/
        public ActionResult Index()
        {
            ViewBag.ShowBack = true;
            return View();
        }
        //
        // GET: /Shop/
        public ActionResult Car()
        {
            ViewBag.ShowBack = true;
            ViewBag.subTitle = "购物车";
            return View();
        }
	}
}