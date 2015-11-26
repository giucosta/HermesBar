﻿using HELPER;
using HermesBarWCF;
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
        private LoginService LoginService = Singleton<LoginService>.Instance();
        public ActionResult Login()
        {
            ViewBag.UsuarioSenhaIncorreto = null;
            return View();
        }
        public ActionResult EfetuarLogin(UsuarioModel login)
        {
            try
            {
                var result = LoginService.EfetuarLogin(login);
                if (result != null)
                {
                    FormsAuthentication.SetAuthCookie(result.PerfilSigla, false);
                    Session["USR"] = result;
                    switch (result.PerfilSigla)
                    {
                        case "ATE":
                            return RedirectToAction("Pedidos", "Pedido");
                        case "ADM":
                            return RedirectToAction("Index", "Home");
                    }
                    return View("Login", new UsuarioModel());
                }
                ViewBag.UsuarioSenhaIncorreto = "Usuário e/ou senha incorretos";
                return View("Login", new UsuarioModel());
            }
            catch (Exception)
            {
                return View("Login", new UsuarioModel());
            }
        }

        public ActionResult EfetuarLogoff()
        {
            FormsAuthentication.SignOut();
            Session["USR"] = null;
            return View("Login");
        }

        #region Private Methods
        private void GetUser()
        {
            var user = (UsuarioModel)Session["USR"];
            ViewBag.User = user.Nome;
        }
        #endregion
    }
}
