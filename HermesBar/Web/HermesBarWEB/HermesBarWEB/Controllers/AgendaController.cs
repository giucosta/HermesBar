using MODEL.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HermesBarWEB.Controllers
{
    public class AgendaController : Controller
    {
        public ActionResult Consultar()
        {
            return View();
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
