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
    public class TipoController : Controller
    {
        private ProductTypeService ProductTypeService = Singleton<ProductTypeService>.Instance();
        private UsuarioModel user;
        public TipoController()
        {
            GetSession.GetUserSession(ref user);
            ViewBag.User = user.Nome;
        }
        public ActionResult Get()
        {
            var listModel = ProductTypeService.Get();
            if(listModel.Count > 0)
                return View(listModel);
            return View(new List<TipoModel>());
        }
        public ActionResult GetJson()
        {
            return Json(ProductTypeService.Get(), JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetId(int id)
        {
            var result = ProductTypeService.GetId(new TipoModel() { Id = id }, user);
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

                if (ProductTypeService.Insert(tipo, user))
                    return View("Get", ProductTypeService.Get());
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
                return ProductTypeService.Insert(new TipoModel() { Nome = nome, Descricao = "", StatusSelected = "1" }, user);
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
                if (ProductTypeService.Update(tipo, user))
                    return View("Get", ProductTypeService.Get());
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
    }
}
