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
            return View();
        }
        //
        // GET: /Shop/
        public ActionResult Car()
        {
            ViewBag.subTitle = "购物车";
            return View();
        }
	}
}