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
    [HmaAuthorize(new int[] { (int)PerfilAuthorize.Perfil.Administrador })]
    public class EstabelecimentoController : Controller
    {
        private HermesBarWCF.EstablishmentService _estabelecimentoService = null;
        private HermesBarWCF.EstablishmentService EstabelecimentoService
        {
            get
            {
                if (_estabelecimentoService == null)
                    _estabelecimentoService = new HermesBarWCF.EstablishmentService();
                return _estabelecimentoService;
            }
        }

        private HermesBarWCF.AddressService _enderecoService = null;
        private HermesBarWCF.AddressService EnderecoService
        {
            get
            {
                if (_enderecoService == null)
                    _enderecoService = new HermesBarWCF.AddressService();
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
                if (estabelecimento.Id == 0)
                {
                    if (EstabelecimentoService.Insert(estabelecimento, user))
                    {
                        ViewBag.Sucesso = true;
                        return View("Configuracoes", EstabelecimentoService.Get(new EstablishmentModel(), user));
                    }
                    ViewBag.Erro = true;
                    return View("Cadastrar", estabelecimento);
                }
                return EditarEstabelecimento(estabelecimento);
            }
            catch (Exception)
            {
                return View("~/Views/Shared/Error.cshtml");
            }
        }
        public ActionResult Inativar(int id)
        {
            try
            {
                if (EstabelecimentoService.Inactive(new EstablishmentModel() { Id = id }, user))
                {
                    ViewBag.InativarSucesso = true;
                    return View("Configuracoes", EstabelecimentoService.Get(new EstablishmentModel(), user));
                }
                ViewBag.InativarErro = true;
                return View("Configuracoes", EstabelecimentoService.Get(new EstablishmentModel(), user));
            }
            catch (Exception)
            {
                ViewBag.InativarErro = true;
                return View("Configuracoes", EstabelecimentoService.Get(new EstablishmentModel(), user));
            }
        }
        public ActionResult Ativar(int id)
        {
            try
            {
                if (EstabelecimentoService.Active(new EstablishmentModel() { Id = id }, user))
                {
                    ViewBag.AtivarSucesso = true;
                    return View("Configuracoes", EstabelecimentoService.Get(new EstablishmentModel(), user));
                }
                ViewBag.AtivarErro = true;
                return View("Configuracoes", EstabelecimentoService.Get(new EstablishmentModel(), user));
            }
            catch (Exception)
            {
                ViewBag.AtivarErro = true;
                return View("Configuracoes", EstabelecimentoService.Get(new EstablishmentModel(), user));
            }
        }

        #region Private Methods
        private ActionResult EditarEstabelecimento(EstablishmentModel estabelecimento)
        {
            try
            {
                if (EstabelecimentoService.Update(estabelecimento, user))
                {
                    ViewBag.EditarSucesso = true;
                    return View("Configuracoes", EstabelecimentoService.Get(new EstablishmentModel(), user));
                }
                ViewBag.EditarErro = true;
                return View("Cadastrar", estabelecimento);
            }
            catch (Exception)
            {
                return View("~/Views/Shared/Error.cshtml");
            }
        }
        private void LoadModel(ref EstablishmentModel model)
        {
            if (model.Contato == null)
                model.Contato = new ContatoModel();

            model.Endereco = EnderecoService.GetStates(model.Endereco);
            if (!string.IsNullOrEmpty(model.Endereco.UfSelected))
            {
                foreach (var item in model.Endereco.UfList)
                {
                    if (model.Endereco.UfSelected == item.Value)
                    {
                        item.Selected = true;
                        break;
                    }
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
