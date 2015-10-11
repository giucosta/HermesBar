using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HermesBarWEB.Controllers
{
    public class ListaComprasController : Controller
    {
        public ActionResult CriarLista()
        {
            return View();
        }
    }
}
