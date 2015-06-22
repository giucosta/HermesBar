using BLL.Login;
using MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HMANet.Controllers
{
    public class LoginController : Controller
    {
        private LoginBLL _loginBLL = null;
        public LoginBLL LoginBLL
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
            ViewBag.LoginSenhaInvalido = null;
            return View();
        }
        public ActionResult EfetuarLogin(FormCollection form)
        {
            var model = new LoginModel();
            model.Login = form["Login"].ToString();
            model.Senha = form["Senha"].ToString();

            if (LoginBLL.EfetuaLogin(model)){
                ViewBag.LoginSenhaInvalido = null;
                return RedirectToAction("Index", "Home");
            }
            else
                ViewBag.LoginSenhaInvalido = "Login e/ou senha inválidos";

            return View("Login");
        }
    }
}
