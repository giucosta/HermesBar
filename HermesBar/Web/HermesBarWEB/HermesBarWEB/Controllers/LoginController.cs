using BLL.User;
using MODEL.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

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
            ViewBag.UsuarioSenhaIncorreto = null;
            return View();
        }
        public ActionResult EfetuarLogin(UsuarioModel login)
        {
            var result = LoginBLL.EfetuarLogin(login);
            if (result != null)
            {
                FormsAuthentication.SetAuthCookie(result.PerfilSigla, false);
                Session["USR"] = result;
                return RedirectToAction("Index", "Home");
            }
            ViewBag.UsuarioSenhaIncorreto = "Usuário e/ou senha incorretos";
            return View("Login", new UsuarioModel());
        }

        private void GetUser()
        {
            var user = (UsuarioModel)Session["USR"];
            ViewBag.User = user.Nome;
        }
    }
}
