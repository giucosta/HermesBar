using HermesBarWCF;
using HermesBarWEB.UTIL;
using MODEL.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HermesBarWEB.Controllers
{
    [HmaAuthorize(new int[] { (int)PerfilAuthorize.Perfil.Administrador })]
    public class UsuarioController : Controller
    {
        private UsuarioService _usuarioService = null;
        private UsuarioService UsuarioService
        {
            get
            {
                if (_usuarioService == null)
                    _usuarioService = new UsuarioService();
                return _usuarioService;
            }
        }

        private UsuarioModel user;
        public UsuarioController()
        {
            GetSession.GetUserSession(ref user);
            ViewBag.User = user.Nome;
        }


        public ActionResult Get()
        {
            return View(UsuarioService.Get(new UsuarioModel()));
        }
    }
}
