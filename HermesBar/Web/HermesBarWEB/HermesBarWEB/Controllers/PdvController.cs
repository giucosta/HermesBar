using HermesBarWEB.UTIL;
using MODEL.PDV.PayBox;
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
        private HermesBarWCF.CaixaService _caixaService = null;
        private HermesBarWCF.CaixaService CaixaService
        {
            get
            {
                if (_caixaService == null)
                    _caixaService = new HermesBarWCF.CaixaService();
                return _caixaService;
            }
        }
        public PdvController()
        {
            GetSession.GetUserSession(ref this._user);
        }

        public ActionResult Index()
        {
            var model = CaixaService.VerifyPayBox();
            LoadSessionPdv(ref model);

            return View();
        }
        public ActionResult AbrirCaixa()
        {
            return View();
        }
        public ActionResult FecharCaixa()
        {
            return View();
        }
        public bool FecharCaixaValor(string valorFinal)
        {
            try
            {
                var model = (PayBoxModel)Session["PDV"];
                
                model.DataFechamento = DateTime.Now;
                model.StatusSelected = "1";
                model.ValorFechamento = Convert.ToDecimal(valorFinal);

                LoadSessionPdv(ref model);
                model.Aberto = !CaixaService.Close(model, _user);
                return model.Aberto;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool AbrirCaixaValor(string valorInicial)
        {
            try
            {
                var model = new PayBoxModel();
                model.DataAbertura = DateTime.Now;
                model.DataFechamento = null;
                model.StatusSelected = "1";
                model.ValorAbertura = Convert.ToDecimal(valorInicial);
                model.ValorFechamento = null;

                LoadSessionPdv(ref model);
                model.Aberto = CaixaService.Open(model, _user);

                return model.Aberto;
            }
            catch (Exception)
            {
                throw;
            }
        }
        private void LoadSessionPdv(ref PayBoxModel model)
        {
            try
            {
                if (model != null)
                    Session["PDV"] = model;
                else
                    Session["PDV"] = new PayBoxModel();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
