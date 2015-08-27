using HermesBarWEB.UTIL;
using MODEL.Event;
using MODEL.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HermesBarWEB.Controllers
{
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

        public ActionResult GetValues()
        {
            try
            {
                return Json(EventoService.Get(new EventModel(), user), JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public ActionResult NovoEvento()
        {
            var evento = new EventModel();
            evento.Cliente = new List<SelectListItem>();
            evento.Cliente.Add(new SelectListItem() { Text = "teste", Value = "1" });
            evento.Status = new List<SelectListItem>();
            evento.Status.Add(new SelectListItem() { Text = "ativo", Value = "1" });
            evento.Data = DateTime.Now;
            return View(evento);
        }
    }
}
