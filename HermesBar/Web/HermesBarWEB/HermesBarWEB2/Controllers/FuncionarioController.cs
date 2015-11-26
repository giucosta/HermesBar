using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HermesBarWEB2.Controllers
{
    public class FuncionarioController : Controller
    {
        public ActionResult Pesquisa()
        {
            ViewBag.Empresa = "Hermes Bar";
            ViewBag.HomeTitle = "HermesBar";
            return View();
        }

    }
}
