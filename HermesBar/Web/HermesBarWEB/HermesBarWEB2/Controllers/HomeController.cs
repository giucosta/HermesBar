using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HermesBarWEB2.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Empresa = "Hermes Bar";
            ViewBag.HomeTitle = "HermesBar";
            return View();
        }
    }
}
