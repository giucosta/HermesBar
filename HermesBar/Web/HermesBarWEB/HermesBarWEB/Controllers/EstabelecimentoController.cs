using BLL.Commom;
using BLL.Establishment;
using HermesBarWEB.UTIL;
using MODEL.Address;
using MODEL.Contact;
using MODEL.Establishment;
using MODEL.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HermesBarWEB.Controllers
{
    public class EstabelecimentoController : Controller
    {
        private HermesBarWCF.EstabelecimentoService _estabelecimentoService = null;
        private HermesBarWCF.EstabelecimentoService EstabelecimentoService
        {
            get
            {
                if (_estabelecimentoService == null)
                    _estabelecimentoService = new HermesBarWCF.EstabelecimentoService();
                return _estabelecimentoService;
            }
        }

        private HermesBarWCF.EnderecoService _enderecoService = null;
        private HermesBarWCF.EnderecoService EnderecoService
        {
            get
            {
                if (_enderecoService == null)
                    _enderecoService = new HermesBarWCF.EnderecoService();
                return _enderecoService;
            }
        }
        
        private UsuarioModel user;
        public EstabelecimentoController()
        {
            GetSession.GetUserSession(ref user);
        }
        
        public ActionResult Configuracoes()
        {
            return View(EstabelecimentoService.Get(new EstablishmentModel(), user));
        }
        public ActionResult Cadastrar()
        {
            var model = new EstablishmentModel();
            LoadModel(ref model);
            return View(model);
        }
        public ActionResult GetId(int id)
        {
            var result = EstabelecimentoService.Get(new EstablishmentModel() { Id = id }, user).FirstOrDefault();
            LoadModel(ref result);
            return View("Cadastrar", result);
        }
        public ActionResult CadastrarEstabelecimento(EstablishmentModel estabelecimento, EnderecoModel endereco, ContatoModel contato)
        {
            try
            {
                estabelecimento.Endereco = endereco;
                estabelecimento.Contato = contato;
                if (EstabelecimentoService.Insert(estabelecimento, user))
                {
                    ViewBag.Sucesso = true;
                    return View("Configuracoes", EstabelecimentoService.Get(new EstablishmentModel(), user));
                }
                ViewBag.Erro = true;
                return View("Cadastrar", estabelecimento);
            }
            catch (Exception)
            {
                return View("~/Views/Shared/Error.cshtml");
            }
        }

        #region Private Methods
        private void LoadModel(ref EstablishmentModel model)
        {
            if (model.Contato == null)
                model.Contato = new ContatoModel();

            var ufs = EnderecoService.GetStates(model.Endereco);
            foreach (var item in ufs.UfList)
            {
                if (model.Endereco.UfSelected == item.Value)
                {
                    item.Selected = true;
                    model.Endereco.UfList = ufs.UfList;
                    break;
                }
            }
            
            model.Status = new List<SelectListItem>();
            foreach (var item in Enum.GetValues(typeof(Enumerators.Status)))
            {
                if (model.StatusSelected == ((int)item).ToString())
                    model.Status.Add(new SelectListItem() { Text = item.ToString(), Value = ((int)item).ToString(), Selected = true });
                else
                    model.Status.Add(new SelectListItem() { Text = item.ToString(), Value = ((int)item).ToString() });
            }
        }
        #endregion
    }
}
