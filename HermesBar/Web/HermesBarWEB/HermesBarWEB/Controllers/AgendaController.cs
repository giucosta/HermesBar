using HermesBarWEB.UTIL;
using MODEL.Client;
using MODEL.Event;
using MODEL.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HermesBarWEB.Controllers
{
    [HmaAuthorize(new int[] { (int)PerfilAuthorize.Perfil.Administrador })]
    public class AgendaController : Controller
    {
        #region Singleton
        private HermesBarWCF.EventoService _eventoService = null;
        private HermesBarWCF.EventoService EventoService
        {
            get
            {
                if (_eventoService == null)
                    _eventoService = new HermesBarWCF.EventoService();
                return _eventoService;
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

        private UsuarioModel user;

        public AgendaController()
        {
            GetSession.GetUserSession(ref user);
            ViewBag.User = user.Nome;
        }

        public ActionResult Consultar()
        {
            return View();
        }
        public ActionResult GetValues(int id = 0)
        {
            try
            {
                return Json(EventoService.Get(new EventModel() { Id = id }, user), JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public ActionResult Editar(int id = 0)
        {
            try
            {
                var model = EventoService.Get(new EventModel() { Id = id }, user).FirstOrDefault();
                LoadModel(ref model);
                return View("NovoEvento", model);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public ActionResult NovoEvento()
        {
            var evento = new EventModel();
            LoadModel(ref evento);
            return View(evento);
        }
        public ActionResult CadastrarEvento(EventModel evento)
        {
            try
            {
                if (evento.Id == 0)
                {
                    if (EventoService.Insert(evento, user))
                    {
                        ViewBag.InsertSuccess = true;
                        return View("Consultar");
                    }
                    ViewBag.InsertError = true;
                    LoadModel(ref evento);
                    return View("NovoEvento", evento);
                }
                return EditarEvento(evento);
            }
            catch (Exception)
            {
                throw;
            }
        }
        private ActionResult EditarEvento(EventModel evento)
        {
            try
            {
                if(EventoService.Update(evento, user))
                {
                    ViewBag.UpdateSuccess = true;
                    return View("Consultar");
                }
                ViewBag.UpdateError = true;
                LoadModel(ref evento);
                return View("NovoEvento", evento);
            }
            catch (Exception)
            {
                throw;
            }
        }
        private void LoadModel(ref EventModel evento)
        {
            try
            {
                var clientes = ClienteService.Get(new ClientModel(), user);
                evento.Cliente = new List<SelectListItem>();
                foreach (var item in clientes)
                {
                    if(evento.ClienteSelected == item.Id.ToString())
                        evento.Cliente.Add(new SelectListItem() { Text = item.Contato.Nome, Value = item.Id.ToString(), Selected = true });
                    else
                        evento.Cliente.Add(new SelectListItem() { Text = item.Contato.Nome, Value = item.Id.ToString() });
                }
                if(evento.Data == DateTime.MinValue)
                    evento.Data = DateTime.Now;

                evento.Status = new List<SelectListItem>();
                foreach (var item in Enum.GetValues(typeof(Enumerators.Status)))
                {
                    if (evento.StatusSelected == ((int)item).ToString())
                        evento.Status.Add(new SelectListItem() { Text = item.ToString(), Value = ((int)item).ToString(), Selected = true });
                    else
                        evento.Status.Add(new SelectListItem() { Text = item.ToString(), Value = ((int)item).ToString() });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
