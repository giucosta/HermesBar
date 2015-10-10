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
    [HmaAuthorize(new int[] { (int)PerfilAuthorize.Perfil.Administrador })]
    public class HomeController : Controller
    {
        private UsuarioModel user;
        public HomeController()
        { 
            GetSession.GetUserSession(ref user);
            ViewBag.User = user.Nome;
        }
        
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetEmails()
        {
            return Json(GetEmail.Get(), JsonRequestBehavior.AllowGet);
        }
    }
}
