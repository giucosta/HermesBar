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
        private UserService _usuarioService = null;
        private UserService UsuarioService
        {
            get
            {
                if (_usuarioService == null)
                    _usuarioService = new UserService();
                return _usuarioService;
            }
        }
        private ProfileService _perfilService = null;
        private ProfileService PerfilService
        {
            get
            {
                if (_perfilService == null)
                    _perfilService = new ProfileService();
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
        public ActionResult GetId(int id)
        {
             var model = UsuarioService.Get(new UsuarioModel() { Id = id }).FirstOrDefault();
            LoadModel(ref model);
            return View("Cadastrar", model);
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
                if (usuario.Id != 0)
                    return EditarUsuario(usuario);

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
        public ActionResult ActiveId(int id)
        {
            try
            {
                if (UsuarioService.Active(new UsuarioModel() { Id = id }))
                    ViewBag.ActiveSuccess = true;
                else
                    ViewBag.ActiveError = true;
                return View("Get", UsuarioService.Get(new UsuarioModel()));

            }
            catch (Exception)
            {
                throw;
            }
        }
        public ActionResult InactiveId(int id)
        {
            try
            {
                if (UsuarioService.Inactive(new UsuarioModel() { Id = id }))
                    ViewBag.InactiveSuccess = true;
                else
                    ViewBag.InactiveError = true;
                return View("Get", UsuarioService.Get(new UsuarioModel()));

            }
            catch (Exception)
            {
                throw;
            }
        }
        private ActionResult EditarUsuario(UsuarioModel usuario)
        {
            try
            {
                if (UsuarioService.Update(usuario))
                {
                    ViewBag.UpdateSuccess = true;
                    return View("Get", UsuarioService.Get(new UsuarioModel()));
                }
                ViewBag.UpdateError = true;
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
                if (user.PerfilSigla == item.Sigla)
                    user.Perfil.Add(new SelectListItem() { Text = item.Descricao.ToString(), Value = item.Id.ToString(), Selected = true });
                else
                    user.Perfil.Add(new SelectListItem() { Text = item.Descricao.ToString(), Value = item.Id.ToString() });
            }
        }
        #endregion
    }
}
