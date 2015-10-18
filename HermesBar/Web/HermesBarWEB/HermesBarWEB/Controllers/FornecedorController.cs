using MODEL.Address;
using MODEL.Contact;
using MODEL.Supplier;
using MODEL.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HermesBarWEB.UTIL;
using HermesBarWCF;
using HELPER;
using MODEL.Establishment;

namespace HermesBarWEB.Controllers
{
    [HmaAuthorize(new int[] { (int)PerfilAuthorize.Perfil.Administrador })]
    public class FornecedorController : Controller
    {
        #region Singleton
        private SupplierService SupplierService = Singleton<SupplierService>.Instance();
        private AddressService AddressService = Singleton<AddressService>.Instance();
        private EstablishmentService EstablishmentService = Singleton<EstablishmentService>.Instance();
        #endregion
        private UsuarioModel user;
        public FornecedorController()
        {
            GetSession.GetUserSession(ref user);
            ViewBag.User = user.Nome;
            ViewBag.Erro = false;
            ViewBag.Sucesso = false;
        }

        
        public ActionResult Get()
        {
            return View(SupplierService.Get(new FornecedorModel() { MatrizSelected = user.MatrizSelected }, user));
        }
        public ActionResult Cadastrar()
        {
            var model = new FornecedorModel();
            LoadModel(ref model);
            return View(model);
        }
        public ActionResult CadastrarFornecedor(FornecedorModel fornecedor, EnderecoModel endereco, ContatoModel contato)
        {
            try
            {
                fornecedor.Contato = contato;
                fornecedor.Endereco = endereco;

                if (fornecedor.Id != 0)
                    return EditarFornecedor(fornecedor);

                if (SupplierService.Insert(fornecedor, user))
                {
                    ViewBag.Sucesso = true;
                    return View("Get", SupplierService.Get(new FornecedorModel() { MatrizSelected = user.MatrizSelected }, user));
                }
                LoadModel(ref fornecedor);
                ViewBag.Erro = true;
                return View("Cadastrar", fornecedor);
            }
            catch (Exception)
            {
                return View("~/Views/Shared/Error.cshtml");
            }
        }
        public ActionResult EditarFornecedor(FornecedorModel fornecedor)
        {
            try
            {
                if (SupplierService.Update(fornecedor, user))
                    return View("Get", SupplierService.Get(new FornecedorModel() { MatrizSelected = user.MatrizSelected }, user));
                return View("Get", SupplierService.Get(new FornecedorModel() { MatrizSelected = user.MatrizSelected }, user));
            }
            catch (Exception)
            {
                return View("~/Views/Shared/Error.cshtml");
            }
        }
        public ActionResult GetId(int id)
        {
            try
            {
                var result = SupplierService.Get(new FornecedorModel() { Id = id, MatrizSelected = user.MatrizSelected }, user).FirstOrDefault();
                result.Endereco = AddressService.GetStates(result.Endereco);
                LoadModel(ref result);
                return View("Cadastrar", result);
            }
            catch (Exception)
            {
                return View("~/Views/Shared/Error.cshtml");
            }
        }

        #region Private Methods
        private void LoadModel(ref FornecedorModel fornecedor)
        {
            if(fornecedor.Contato == null)
                fornecedor.Contato = new ContatoModel();
            if (fornecedor.Endereco == null)
                fornecedor.Endereco = AddressService.GetStates(new EnderecoModel());
            else
                fornecedor.Endereco = AddressService.GetStates(fornecedor.Endereco);

            fornecedor.Matriz = new List<SelectListItem>();
            foreach (var item in EstablishmentService.Get(new EstablishmentModel(), user))
            {
                if (fornecedor.MatrizSelected == item.Id.ToString())
                    fornecedor.Matriz.Add(new SelectListItem() { Text = item.RazaoSocial, Value = item.Id.ToString(), Selected = true });
                else
                    fornecedor.Matriz.Add(new SelectListItem() { Text = item.RazaoSocial, Value = item.Id.ToString() });
            }
        }
        #endregion
    }
}
