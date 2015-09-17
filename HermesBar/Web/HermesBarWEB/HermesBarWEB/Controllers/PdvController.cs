using HermesBarWEB.UTIL;
using MODEL.Client;
using MODEL.PDV.Client;
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
        #region Singleton
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
        private HermesBarWCF.PdvClienteService _pdvClienteService = null;
        private HermesBarWCF.PdvClienteService PdvClienteService
        {
            get
            {
                if (_pdvClienteService == null)
                    _pdvClienteService = new HermesBarWCF.PdvClienteService();
                return _pdvClienteService;
            }
        }
        private HermesBarWCF.ClienteService _clienteService = null;
        private HermesBarWCF.ClienteService ClienteService
        {
            get
            {
                if (_clienteService == null)
                    _clienteService = new HermesBarWCF.ClienteService();
                return _clienteService;
            }
        }
        #endregion
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
        public bool FecharCaixaValor(string valorFinal)
        {
            try
            {
                var model = GetSessionPdv();
                if (model.Aberto)
                {
                    model.DataFechamento = DateTime.Now;
                    model.StatusSelected = "1";
                    model.ValorFechamento = Convert.ToDecimal(valorFinal);

                    LoadSessionPdv(ref model);
                    model.Aberto = !CaixaService.Close(model, _user);
                    return model.Aberto;
                }
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public ActionResult EntradaCliente()
        {
            return View();
        }
        public bool EntradaClienteCadastro(string id, string rg, string nome, string telefone, string nascimento, string numeroCartao)
        {
            if (GetSessionPdv().Aberto)
            {
                var model = new PdvClientModel();
                if (string.IsNullOrEmpty(id))
                {
                    var clienteModel = new ClientModel();
                    clienteModel.RG = rg;
                    clienteModel.Contato = new MODEL.Contact.ContatoModel();
                    clienteModel.Contato.Nome = nome;
                    clienteModel.Contato.Celular = telefone;
                    clienteModel.DataNascimento = Convert.ToDateTime(nascimento);
                    clienteModel.StatusSelected = "1";

                    if (ClienteService.Insert(clienteModel, _user, true))
                        id = ClienteService.Get(clienteModel, _user).FirstOrDefault().Id.ToString();
                    else
                        return false;
                }
                model.IdCliente = Convert.ToInt32(id);
                model.Entrada = DateTime.Now;
                model.ConsumoTotal = 0;
                model.IdCaixa = GetSessionPdv().Id;
                model.Saida = DateTime.Now;
                model.NumeroCartao = Convert.ToInt32(numeroCartao);

                return PdvClienteService.Insert(model, _user);
            }
            return false;
        }
        public ActionResult ReforcoCaixa()
        {
            return View();
        }
        public bool AdicionarReforco(string valorReforco, string motivo)
        {
            try
            {
                var model = new PayBoxModel();
                model.Id = GetSessionPdv().Id;
                model.ValorReforco = Convert.ToDecimal(valorReforco);
                model.Descricao = motivo;
                
                return CaixaService.Reinforcement(model, _user);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public ActionResult SangriaCaixa()
        {
            return View();
        }
        public bool EfetuarSangria(string valorSangria, string motivo)
        {
            try
            {
                var model = new PayBoxModel();
                model.Id = GetSessionPdv().Id;
                model.ValorSangria = Convert.ToDecimal(valorSangria);
                model.Descricao = motivo;
                
                return CaixaService.Depletion(model, _user);
            }
            catch (Exception)
            {
                throw;
            }
        }

        #region Private Methods
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
        private PayBoxModel GetSessionPdv()
        {
            try
            {
                return (PayBoxModel)Session["PDV"];
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}
