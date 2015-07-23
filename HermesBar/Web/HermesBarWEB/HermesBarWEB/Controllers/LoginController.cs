using BLL.User;
using MODEL.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HermesBarWEB.Controllers
{
    public class LoginController : Controller
    {
        private LoginBLL _loginBLL = null;
        private LoginBLL LoginBLL
        {
            get
            {
                if (_loginBLL == null)
                    _loginBLL = new LoginBLL();
                return _loginBLL;
            }
        }
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult EfetuarLogin(UsuarioModel login)
        {
            var result = LoginBLL.EfetuarLogin(login);
            if (result != null)
            {
                Session["USR"] = result;
                return RedirectToAction("Index", "Home");
            }
            return View("Login");
        }

        private void GetUser()
        {
            var user = (UsuarioModel)Session["USR"];
            ViewBag.User = user.Nome;
        }
    }
}
