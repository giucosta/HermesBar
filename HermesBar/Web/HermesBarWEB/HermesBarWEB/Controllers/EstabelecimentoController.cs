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
        private EstablishmentBLL _establishmentBLL = null;
        private EstablishmentBLL EstablishmentBLL
        {
            get
            {
                if (_establishmentBLL == null)
                    _establishmentBLL = new EstablishmentBLL();
                return _establishmentBLL;
            }
        }
        private EnderecoBLL _enderecoBLL = null;
        private EnderecoBLL EnderecoBLL
        {
            get
            {
                if (_enderecoBLL == null)
                    _enderecoBLL = new EnderecoBLL();
                return _enderecoBLL;
            }
        }
        private UsuarioModel user;
        public EstabelecimentoController()
        {
            GetSession.GetUserSession(ref user);
        }
        
        public ActionResult Configuracoes()
        {
            return View(EstablishmentBLL.Get(new EstablishmentModel(), user));
        }
        public ActionResult Cadastrar()
        {
            var model = new EstablishmentModel();
            LoadModel(ref model);
            return View(model);
        }
        public ActionResult CadastrarEstabelecimento(EstablishmentModel estabelecimento)
        {
            return View("Configuracoes");
        }
        private void LoadModel(ref EstablishmentModel model)
        {
            model.Contato = new ContatoModel();
            model.Endereco = EnderecoBLL.GetStates(new EnderecoModel());

            model.Status = new List<SelectListItem>();
            foreach (var item in Enum.GetValues(typeof(Enumerators.Status)))
            {
                if (model.StatusSelected == ((int)item).ToString())
                    model.Status.Add(new SelectListItem() { Text = item.ToString(), Value = ((int)item).ToString(), Selected = true });
                else
                    model.Status.Add(new SelectListItem() { Text = item.ToString(), Value = ((int)item).ToString() });
            }
        }
    }
}
