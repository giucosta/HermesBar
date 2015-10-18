﻿using HELPER;
using HermesBarWCF;
using HermesBarWEB.UTIL;
using MODEL.Client;
using MODEL.PDV.Client;
using MODEL.PDV.PayBox;
using MODEL.PDV.Session;
using MODEL.Product;
using MODEL.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HermesBarWEB.Controllers
{
    [HmaAuthorize(new int[] { (int)PerfilAuthorize.Perfil.Administrador })]
    public class PdvController : Controller
    {
        #region Singleton
        private UsuarioModel _user;
        private PayBoxService PayBoxService = Singleton<PayBoxService>.Instance();
        private PdvClientService PdvClientService = Singleton<PdvClientService>.Instance();
        private ClientService ClientService = Singleton<ClientService>.Instance();
        private EmployeeService EmployeeService = Singleton<EmployeeService>.Instance();
        private ProductService ProductService = Singleton<ProductService>.Instance();
        #endregion
        public PdvController()
        {
            GetSession.GetUserSession(ref this._user);
        }

        public ActionResult Index()
        {
            var model = PayBoxService.VerifyPayBox();
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
                model.Aberto = PayBoxService.Open(model, _user);

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
                    model.Aberto = !PayBoxService.Close(model, _user);
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
        public ActionResult SaidaCliente()
        {
            return View();
        }
        public int EntradaClienteCadastro(string id, string rg, string nome, string telefone, string nascimento, string numeroCartao)
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

                    if (ClientService.Insert(clienteModel, _user, true))
                        id = ClientService.Get(clienteModel, _user).FirstOrDefault().Id.ToString();
                    else
                        return -3;
                }
                model.IdCliente = Convert.ToInt32(id);
                model.Entrada = DateTime.Now;
                model.ConsumoTotal = 0;
                model.IdCaixa = GetSessionPdv().Id;
                model.Saida = DateTime.Now;
                model.NumeroCartao = Convert.ToInt32(numeroCartao);

                return PdvClientService.Insert(model, _user);
            }
            return -3;
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
                
                return PayBoxService.Reinforcement(model, _user);
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
                
                return PayBoxService.Depletion(model, _user);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public string GetClienteCartao(string numeroCartao)
        {
            try
            {
                return PdvClientService.GetCard(new PdvClientModel() { NumeroCartao = Convert.ToInt32(numeroCartao) }, _user);
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
        public ActionResult GetFuncionarioId(string idFuncionario)
        {
            try
            {
                return Json(EmployeeService.Get(
                            new MODEL.Employee.EmployeeModel() { 
                                Id = Convert.ToInt32(idFuncionario), 
                                Cpf = "", 
                                MatrizSelected = _user.MatrizSelected, 
                                StatusSelected = 1.ToString() 
                            }, 
                        _user), JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public ActionResult GetProdutoId(string idProduto)
        {
            try
            {
                if (idProduto == "")
                    return Json(new ProdutoModel(), JsonRequestBehavior.AllowGet);
                return Json(ProductService.GetId(Convert.ToInt32(idProduto), (int)HermesBarWEB.UTIL.Enumerators.Status.Ativo), JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public ActionResult InserirPedido(string cartaoCliente, string codigoAtendente, string nomeProduto, string quantidade)
        {
            try
            {

                return Json(PdvClientService.Order(cartaoCliente, codigoAtendente, nomeProduto, quantidade, _user, GetSessionPdv().Id), JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public ActionResult Fechamento(string numeroCartao)
        {
            if (numeroCartao == "")
                return Json(new List<PdvFechamentoClientModel>(), JsonRequestBehavior.AllowGet);

            var fechamentoModel = new PdvClientModel();
            fechamentoModel.NumeroCartao = Convert.ToInt32(numeroCartao);
            fechamentoModel.IdCaixa = GetSessionPdv().Id;

            return Json(PdvClientService.Close(fechamentoModel), JsonRequestBehavior.AllowGet);
        }
        public ActionResult FecharComanda(string numeroCartao, string valorTotal, string valorRecebido, string troco, string formaPagamento)
        {
            return Json(PdvClientService.CloseCommands(ConvertToModel(numeroCartao, valorTotal, valorRecebido, troco, formaPagamento), _user), JsonRequestBehavior.AllowGet);
        }

        
        #region Private Methods
        private PdvClientModel ConvertToModel(string numeroCartao, string valorTotal, string valorRecebido, string troco, string formaPagamento)
        {
            try
            {
                var model = new PdvClientModel();
                model.NumeroCartao = Convert.ToInt32(numeroCartao);
                model.IdCaixa = GetSessionPdv().Id;
                model.Troco = Convert.ToDecimal(troco);
                model.ConsumoTotal = Convert.ToDecimal(valorTotal);
                model.ValorRecebido = Convert.ToDecimal(valorRecebido);
                model.FormaPagamento = formaPagamento;

                return model;
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
