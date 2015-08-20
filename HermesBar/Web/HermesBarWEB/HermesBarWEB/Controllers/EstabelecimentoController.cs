using BLL.Establishment;
using HermesBarWEB.UTIL;
using MODEL.Establishment;
using MODEL.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HermesBarWEB.Controllers
{
    public class EstabelecimentoController : Controller
    {
        private EstablishmentBLL _establishmentBLL = null;
        private EstablishmentBLL EstablishmentBLL
        {
            get
            {
                if (_establishmentBLL == null)
                    _establishmentBLL = new EstablishmentBLL();
                return _establishmentBLL;
            }
        }
        private UsuarioModel user;
        public EstabelecimentoController()
        {
            GetSession.GetUserSession(ref user);
        }
        public ActionResult Configuracoes()
        {
            return View(EstablishmentBLL.Get(new EstablishmentModel(), user));
        }


    }
}
