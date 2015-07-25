using BLL.User;
using BLL.UTIL;
using HermesBarWEB.UTIL;
using MODEL.Commom;
using MODEL.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HermesBarWEB.Controllers
{
    public class HomeController : Controller
    {
        private UsuarioModel user;
        public HomeController()
        { 
            GetSession.GetUserSession(ref user);
            ViewBag.User = user.Nome;
            ViewBag.Email = GetEmail.Get();
        }
        [HmaAuthorize(new int[] { (int)PerfilAuthorize.Perfil.Administrador})]
        public ActionResult Index()
        {
            return View();
        }
    }
}
