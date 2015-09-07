using HermesBarWEB.UTIL;
using MODEL.PDV.Session;
using MODEL.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HermesBarWEB.Controllers
{
    public class PdvController : Controller
    {
        private UsuarioModel _user;
        public PdvController()
        {
            GetSession.GetUserSession(ref this._user);
        }
        public ActionResult Index()
        {
            LoadSession();
            return View();
        }

        public ActionResult AbrirCaixa()
        {
            return View();
        }
        public bool AbrirCaixaValor(string valorInicial)
        {
            try
            {
                ((PdvSession)Session["PDV"]).ValorInicial = Convert.ToDecimal(valorInicial);
                ((PdvSession)Session["PDV"]).DataAbertura = DateTime.Now;
                ((PdvSession)Session["PDV"]).Usuario = _user.Id;
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #region Private Methods
        private void LoadSession()
        {
            if ((PdvSession)Session["PDV"] == null)
                Session["PDV"] = new PdvSession();
        }
        #endregion
    }
}
