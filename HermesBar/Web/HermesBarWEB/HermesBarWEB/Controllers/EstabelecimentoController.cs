using HELPER;
using HermesBarWCF;
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
        private EstablishmentService EstablishmentService = Singleton<EstablishmentService>.Instance();
        private AddressService AddressService = Singleton<AddressService>.Instance();
        
        private UsuarioModel user;
        public EstabelecimentoController()
        {
            GetSession.GetUserSession(ref user);
        }
        
        public ActionResult Configuracoes()
        {
            try
            {
                return View(EstablishmentService.Get(new EstablishmentModel(), user));
            }
            catch (Exception)
            {
                return View("Error");
            }
        }
        public ActionResult Cadastrar()
        {
            try
            {
                var model = new EstablishmentModel();
                LoadModel(ref model);
                return View(model);
            }
            catch (Exception)
            {   
                return View("Error");
            }
        }
        public ActionResult GetId(int id)
        {
            try
            {
                var result = EstablishmentService.Get(new EstablishmentModel() { Id = id }, user).FirstOrDefault();
                LoadModel(ref result);
                return View("Cadastrar", result);
            }
            catch (Exception)
            {
                return View("Error");
            }
        }
        public ActionResult CadastrarEstabelecimento(EstablishmentModel estabelecimento, EnderecoModel endereco, ContatoModel contato)
        {
            try
            {
                estabelecimento.Endereco = endereco;
                estabelecimento.Contato = contato;
                if (estabelecimento.Id == 0)
                {
                    if (EstablishmentService.Insert(estabelecimento, user))
                    {
                        ViewBag.Sucesso = true;
                        return View("Configuracoes", EstablishmentService.Get(new EstablishmentModel(), user));
                    }
                    ViewBag.Erro = true;
                    return View("Cadastrar", estabelecimento);
                }
                return EditarEstabelecimento(estabelecimento);
            }
            catch (Exception)
            {
                ViewBag.Erro = true;
                return View("Cadastrar", estabelecimento);
            }
        }
        public ActionResult Inativar(int id)
        {
            try
            {
                if (EstablishmentService.Inactive(new EstablishmentModel() { Id = id }, user))
                {
                    ViewBag.InativarSucesso = true;
                    return View("Configuracoes", EstablishmentService.Get(new EstablishmentModel(), user));
                }
                ViewBag.InativarErro = true;
                return View("Configuracoes", EstablishmentService.Get(new EstablishmentModel(), user));
            }
            catch (Exception)
            {
                ViewBag.InativarErro = true;
                return View("Configuracoes", EstablishmentService.Get(new EstablishmentModel(), user));
            }
        }
        public ActionResult Ativar(int id)
        {
            try
            {
                if (EstablishmentService.Active(new EstablishmentModel() { Id = id }, user))
                {
                    ViewBag.AtivarSucesso = true;
                    return View("Configuracoes", EstablishmentService.Get(new EstablishmentModel(), user));
                }
                ViewBag.AtivarErro = true;
                return View("Configuracoes", EstablishmentService.Get(new EstablishmentModel(), user));
            }
            catch (Exception)
            {
                ViewBag.AtivarErro = true;
                return View("Configuracoes", EstablishmentService.Get(new EstablishmentModel(), user));
            }
        }

        #region Private Methods
        private ActionResult EditarEstabelecimento(EstablishmentModel estabelecimento)
        {
            try
            {
                if (EstablishmentService.Update(estabelecimento, user))
                {
                    ViewBag.EditarSucesso = true;
                    return View("Configuracoes", EstablishmentService.Get(new EstablishmentModel(), user));
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

            model.Endereco = AddressService.GetStates(model.Endereco);
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
