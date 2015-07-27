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
    public class TipoController : Controller
    {
        private TypeBLL _typeBLL = null;
        private TypeBLL TypeBLL
        {
            get
            {
                if (_typeBLL == null)
                    _typeBLL = new TypeBLL();
                return _typeBLL;
            }
        }
        public ActionResult Get()
        {
            var listModel = TypeBLL.Get();
            if(listModel.Count > 0)
                return View(listModel);
            return View(new List<TipoModel>());
        }
        public ActionResult Cadastrar()
        {
            var model = new TipoModel();
            CarregaCampos(ref model);
            return View(model);
        }
        public ActionResult CadastrarTipo(TipoModel tipo)
        {
            try
            {
                if (TypeBLL.Insert(tipo, GetUser()))
                    return View("Get", TypeBLL.Get());
                return View("Cadastrar", tipo);
                    
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CarregaCampos(ref TipoModel tipo)
        {
            tipo.Status = new List<SelectListItem>();
            tipo.Status.Add(new SelectListItem() { Text = Enumerators.Status.Ativo.ToString(), Value = ((int)Enumerators.Status.Ativo).ToString(), Selected = true });
            tipo.Status.Add(new SelectListItem() { Text = Enumerators.Status.Inativo.ToString(), Value = ((int)Enumerators.Status.Inativo).ToString() });
        }
        private UsuarioModel GetUser()
        {
            return (UsuarioModel)Session["USR"];
        }
    }
}
