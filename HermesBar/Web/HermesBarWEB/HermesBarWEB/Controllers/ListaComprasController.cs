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

        public ActionResult Get()
        {
            return View(ShoppingListService.Get());
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
        public ActionResult IniciarCompra(int id)
        {
            try
            {
                return View(ShoppingListService.GetId(id));
            }
            catch (Exception)
            {
                throw;
            }
        }
        public ActionResult ItemComprado(string quantidade, string valorUnidade, string idProduto, string idLista)
        {
            try
            {
                var model = new ListaComprasModel();
                model.quantidade = quantidade;
                model.id = idProduto;
                model.IdLista = Convert.ToInt32(idLista);

                return Json(ShoppingListService.InsertPurchase(model, user, Convert.ToInt32(idLista)), JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
