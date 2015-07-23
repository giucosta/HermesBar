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
        public ActionResult Index()
        {
            GetUser();
            return View();
        }
        private void GetUser()
        {
            var user = (UsuarioModel)Session["USR"];
            ViewBag.User = user.Nome;
        }
    }
}
