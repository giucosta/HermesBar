using HermesBarWEB.UTIL;
using MODEL.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HermesBarWEB.Controllers
{
    public class ProdutoController : Controller
    {
        private UsuarioModel user;
        public ProdutoController()
        {
            GetSession.GetUserSession(ref user);
            ViewBag.User = user.Nome;
        }
        public ActionResult Get()
        {
            return View();
        }
    }
}
