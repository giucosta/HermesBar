using HELPER;
using HermesBarWCF;
using HermesBarWEB.UTIL;
using MODEL.Product;
using MODEL.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HermesBarWEB.Controllers
{
    [HmaAuthorize(new int[] { (int)PerfilAuthorize.Perfil.Administrador })]
    public class UnidadeMedidaController : Controller
    {
        private ProductUnitySizeService ProductUnitySizeService = Singleton<ProductUnitySizeService>.Instance();
        private UsuarioModel user;
        public UnidadeMedidaController()
        {
            GetSession.GetUserSession(ref user);
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetJson()
        {
            return Json(ProductUnitySizeService.Get(), JsonRequestBehavior.AllowGet);
        }
        public bool CadastroRapido(string nome)
        {
            try
            {
                return ProductUnitySizeService.Insert(new UnidadeMedidaModel() { Nome = nome, Descricao = "", StatusSelected = "1" }, user);
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
