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
        public ActionResult GetId(int id)
        {
            var model = ProdutoBLL.GetId(id);
            LoadModel(ref model);
            return View("Cadastrar", model);
        }
        public ActionResult CadastrarProduto(ProdutoModel produto)
        {
            try
            {
                if (!ProdutoBLL.Insert(produto, GetUser()))
                {
                    ViewBag.Error = "Ocorreu um erro ao salvar o produto";
                    return View("Cadastrar", produto);
                }
                ViewBag.Success = "Produto cadastrado com sucesso!";
                return View("Get", ProdutoBLL.Get());
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Ocorreu um erro ao salvar o produto " + ex.Message;
                return View("Cadastrar", produto);
            }
        }

        #region Private Methods
        private void LoadModel(ref ProdutoModel model)
        {
            if (string.IsNullOrEmpty(model.CodigoVenda))
                model.CodigoVenda = ProdutoBLL.GetNextCode().ToString();

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
            model.Status = new List<SelectListItem>();
            foreach (var item in Enum.GetValues(typeof(Enumerators.Status)))
            {
                if (model.StatusSelected == ((int)item).ToString())
                    model.Status.Add(new SelectListItem() { Text = item.ToString(), Value = ((int)item).ToString(), Selected = true });
                else
                    model.Status.Add(new SelectListItem() { Text = item.ToString(), Value = ((int)item).ToString() });
            }
        }
        private UsuarioModel GetUser()
        {
            return (UsuarioModel)Session["USR"];
        }
        #endregion
    }
}
