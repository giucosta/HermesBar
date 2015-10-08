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
        public ActionResult Cadastrar()
        {
            return View();
        }

        #region Private Methods
        private void LoadModel(ref UsuarioModel user)
        {
            user.Status = new List<SelectListItem>();
            foreach (var item in Enum.GetValues(typeof(Enumerators.Status)))
            {
                if (user.StatusSelected == ((int)item).ToString())
                    user.Status.Add(new SelectListItem() { Text = item.ToString(), Value = ((int)item).ToString(), Selected = true });
                else
                    user.Status.Add(new SelectListItem() { Text = item.ToString(), Value = ((int)item).ToString() });
            }
            user.Perfil = new List<SelectListItem>();
            foreach (var item in collection)
            {
                
            }
        }
        #endregion
    }
}
