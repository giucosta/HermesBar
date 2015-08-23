using HermesBarWEB.UTIL;
using MODEL.Client;
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
            return View(ClienteService.Get(new ClientModel(), user));
        }
    }
}
