using HermesBarWEB.UTIL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HermesBarWEB.Controllers
{
    [HmaAuthorize(new int[] { (int)PerfilAuthorize.Perfil.Administrador })]
    public class EstoqueController : Controller
    {
        public ActionResult Consultar()
        {
            return View();
        }
    }
}
