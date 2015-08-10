using BLL.Product;
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
    public class ProdutoController : Controller
    {
        private ProductBLL _produtoBLL = null;
        private ProductBLL ProdutoBLL
        {
            get
            {
                if (_produtoBLL == null)
                    _produtoBLL = new ProductBLL();
                return _produtoBLL;
            }
        }
        private UsuarioModel user;
        public ProdutoController()
        {
            GetSession.GetUserSession(ref user);
            ViewBag.User = user.Nome;
        }
        public ActionResult Get()
        {
            return View(ProdutoBLL.Get());
        }
        public ActionResult Cadastrar()
        {
            var model = new ProdutoModel();
            LoadModel(ref model);
            return View(model);
        }

        #region Private Methods
        private void LoadModel(ref ProdutoModel model)
        {
            model.Tipos = new List<SelectListItem>();
            var tipos = new TypeBLL().Get();
            foreach (var item in tipos)
            {
                if (item.Id.ToString() == model.TipoSelected)
                    model.Tipos.Add(new SelectListItem() { Text = item.Nome, Value = item.Id.ToString(), Selected = true });
                else
                    model.Tipos.Add(new SelectListItem() { Text = item.Nome, Value = item.Id.ToString() });
            }

            model.UnidadesMedida = new List<SelectListItem>();
            var unidades = new UniMedBLL().Get();
            foreach (var item in unidades)
            {
                if (item.Id.ToString() == model.UnidadeMedidaSelected)
                    model.UnidadesMedida.Add(new SelectListItem() { Text = item.Nome, Value = item.Id.ToString(), Selected = true });
                else
                    model.UnidadesMedida.Add(new SelectListItem() { Text = item.Nome, Value = item.Id.ToString() });
            }
        }
        #endregion
    }
}
