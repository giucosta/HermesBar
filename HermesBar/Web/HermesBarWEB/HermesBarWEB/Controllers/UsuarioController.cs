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
        #region Singleton
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
        private PerfilService _perfilService = null;
        private PerfilService PerfilService
        {
            get
            {
                if (_perfilService == null)
                    _perfilService = new PerfilService();
                return _perfilService;
            }
        }
        #endregion
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
            var model = new UsuarioModel();
            LoadModel(ref model);
            return View(model);
        }
        public ActionResult CadastrarUsuario(UsuarioModel usuario)
        {
            try
            {
                usuario.Id = user.Id;
                if (UsuarioService.Insert(usuario))
                    return View("Get", UsuarioService.Get(new UsuarioModel()));

                LoadModel(ref usuario);
                return View("Cadastrar", usuario);
            }
            catch (Exception)
            {
                throw;
            }
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
            foreach (var item in PerfilService.Get())
            {
                if (user.PerfilSelected == item.Id.ToString())
                    user.Perfil.Add(new SelectListItem() { Text = item.Descricao.ToString(), Value = item.Id.ToString(), Selected = true });
                else
                    user.Perfil.Add(new SelectListItem() { Text = item.Descricao.ToString(), Value = item.Id.ToString() });
            }
        }
        #endregion
    }
}
