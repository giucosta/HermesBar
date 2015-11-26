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
    public class DocumentacaoController : Controller
    {
         private UsuarioModel user;
         public DocumentacaoController()
        {
            GetSession.GetUserSession(ref user);
        }
        public ActionResult Index()
        {
            return View();
        }
    }
}
