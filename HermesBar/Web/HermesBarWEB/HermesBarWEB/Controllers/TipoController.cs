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
        public ActionResult GetJson()
        {
            return Json(TypeBLL.Get(), JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetId(int id)
        {
            var result = TypeBLL.GetId(new TipoModel() { Id = id }, GetUser());
            CarregaCampos(ref result);
            return View("Cadastrar", result);
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
                if (tipo.Id != 0)
                    return Editar(tipo);

                if (TypeBLL.Insert(tipo, GetUser()))
                    return View("Get", TypeBLL.Get());
                return View("Cadastrar", tipo);
                    
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool CadastroRapido(string nome)
        {
            try
            {
                return TypeBLL.Insert(new TipoModel() { Nome = nome, Descricao = "", StatusSelected = "1" }, GetUser());
            }
            catch (Exception)
            {
                return false;
            }
        }
        public ActionResult Editar(TipoModel tipo)
        {
            try
            {
                if (TypeBLL.Update(tipo, GetUser()))
                    return View("Get", TypeBLL.Get());
                return View("Cadastrar", tipo);
            }
            catch (Exception)
            {
                return View("~/Views/Shared/Error.cshtml");
            }
        }
        private void CarregaCampos(ref TipoModel tipo)
        {
            tipo.Status = new List<SelectListItem>();
            foreach (var item in Enum.GetValues(typeof(Enumerators.Status)))
            {
                if (tipo.StatusSelected == ((int)item).ToString())
                    tipo.Status.Add(new SelectListItem() { Text = item.ToString(), Value = ((int)item).ToString(), Selected = true });
                else
                    tipo.Status.Add(new SelectListItem() { Text = item.ToString(), Value = ((int)item).ToString() });
            }

        }
        private UsuarioModel GetUser()
        {
            return (UsuarioModel)Session["USR"];
        }
    }
}
