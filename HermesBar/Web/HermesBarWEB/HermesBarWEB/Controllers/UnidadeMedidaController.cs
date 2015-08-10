using BLL.Product;
using MODEL.Product;
using MODEL.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HermesBarWEB.Controllers
{
    public class UnidadeMedidaController : Controller
    {
        private UniMedBLL _unidadeMedidaBLL = null;
        private UniMedBLL UnidadeMedidaBLL
        {
            get
            {
                if (_unidadeMedidaBLL == null)
                    _unidadeMedidaBLL = new UniMedBLL();
                return _unidadeMedidaBLL;
            }
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetJson()
        {
            return Json(UnidadeMedidaBLL.Get(), JsonRequestBehavior.AllowGet);
        }
        public bool CadastroRapido(string nome)
        {
            try
            {
                return UnidadeMedidaBLL.Insert(new UnidadeMedidaModel() { Nome = nome, Descricao = "", StatusSelected = "1" }, GetUser());
            }
            catch (Exception)
            {
                return false;
            }
        }
        private UsuarioModel GetUser()
        {
            return (UsuarioModel)Session["USR"];
        }
    }
}
