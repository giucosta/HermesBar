using HELPER;
using HermesBarWCF;
using HermesBarWEB.UTIL;
using MODEL.ListaCompras;
using MODEL.User;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HermesBarWEB.Controllers
{
    public class ListaComprasController : Controller
    {
        private ShoppingListService ShoppingListService = Singleton<ShoppingListService>.Instance();
        private UsuarioModel user;

        public ListaComprasController()
        {
            GetSession.GetUserSession(ref this.user);
        }
        public ActionResult CriarLista()
        {
            return View();
        }
        public ActionResult CadastrarListaCompra(string lista)
        {
            try
            {
                return Json(ShoppingListService.Insert(
                    JsonConvert.DeserializeObject<List<ListaComprasModel>>(lista), 
                    user), 
                    JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
