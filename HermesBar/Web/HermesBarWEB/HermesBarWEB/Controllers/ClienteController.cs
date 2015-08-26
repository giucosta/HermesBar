using HermesBarWEB.UTIL;
using MODEL.Client;
using MODEL.Contact;
using MODEL.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HermesBarWEB.Controllers
{
    public class ClienteController : Controller
    {
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

        private UsuarioModel user;
        public ClienteController()
        {
            GetSession.GetUserSession(ref user);
            ViewBag.User = user.Nome;
        }
        public ActionResult Get()
        {
            try
            {
                return View(ClienteService.Get(new ClientModel(), user));
            }
            catch (Exception)
            {
                throw;
            }
        }
        public ActionResult GetId(int id)
        {
            try
            {
                var model = ClienteService.Get(new ClientModel() { Id = id }, user).FirstOrDefault();
                LoadModel(ref model);
                return View("Cadastrar", model);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public ActionResult Cadastrar()
        {
            try
            {
                var model = new ClientModel();
                LoadModel(ref model);
                return View(model);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public ActionResult CadastrarCliente(ClientModel cliente, ContatoModel contato)
        {
            try
            {
                cliente.Contato = contato;
                if (cliente.Id != 0)
                    return Editar(cliente);

                if (ClienteService.Insert(cliente, user))
                {
                    ViewBag.InsertSuccess = true;
                    return RedirectToAction("Get", "Cliente");
                }
                ViewBag.InsertError = true;
                return View("Cadastrar", cliente);
            }
            catch (Exception ex)
            {
                ViewBag.RgCadastrado = ex.Message;
                LoadModel(ref cliente);
                return View("Cadastrar", cliente);
            }
        }
        public ActionResult InactiveId(int id)
        {
            try
            {
                if (ClienteService.Inactive(new ClientModel() { Id =id }, user))
                    ViewBag.InactiveSuccess = true;
                else
                    ViewBag.InactiveError = true;
                return View("Get", ClienteService.Get(new ClientModel(), user));
            }
            catch (Exception)
            {
                throw;
            }
        }
        public ActionResult ActiveId(int id)
        {
            try
            {
                if (ClienteService.Active(new ClientModel() { Id = id }, user))
                    ViewBag.ActiveSuccess = true;
                else
                    ViewBag.ActiveError = true;
                return View("Get", ClienteService.Get(new ClientModel(), user));
            }
            catch (Exception)
            {
                throw;
            }
        }

        private ActionResult Editar(ClientModel client)
        {
            try
            {
                if (ClienteService.Update(client, user))
                    ViewBag.UpdateSuccess = true;
                else
                {
                    ViewBag.UpdateError = true;
                    LoadModel(ref client);
                    return View("Cadastrar", client);
                }
                return View("Get", ClienteService.Get(new ClientModel(), user));
            }
            catch (Exception)
            {
                throw;
            }
        }
        private void LoadModel(ref ClientModel cliente)
        {
            try
            {
                if(cliente.Contato == null)
                    cliente.Contato = new MODEL.Contact.ContatoModel();
                
                if(cliente.DataNascimento == DateTime.MinValue)
                    cliente.DataNascimento = DateTime.Now;

                cliente.Status = new List<SelectListItem>();
                foreach (var item in Enum.GetValues(typeof(Enumerators.Status)))
                {
                    if (cliente.StatusSelected == ((int)item).ToString())
                        cliente.Status.Add(new SelectListItem() { Text = item.ToString(), Value = ((int)item).ToString(), Selected = true });
                    else
                        cliente.Status.Add(new SelectListItem() { Text = item.ToString(), Value = ((int)item).ToString() });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
