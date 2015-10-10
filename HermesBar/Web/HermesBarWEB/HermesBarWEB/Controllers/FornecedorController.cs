using BLL.Supplier;
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
using BLL.UTIL;
using BLL.Commom;

namespace HermesBarWEB.Controllers
{
    [HmaAuthorize(new int[] { (int)PerfilAuthorize.Perfil.Administrador })]
    public class FornecedorController : Controller
    {
        private UsuarioModel user;
        public FornecedorController()
        {
            GetSession.GetUserSession(ref user);
            ViewBag.User = user.Nome;
            ViewBag.Erro = false;
            ViewBag.Sucesso = false;
        }

        #region Singleton
        private FornecedorBLL _fornecedorBLL = null;
        private FornecedorBLL FornecedorBLL
        {
            get
            {
                if (_fornecedorBLL == null)
                    _fornecedorBLL = new FornecedorBLL();
                return _fornecedorBLL;
            }
        }
        private AddressBLL _enderecoBLL = null;
        private AddressBLL EnderecoBLL
        {
            get
            {
                if (_enderecoBLL == null)
                    _enderecoBLL = new AddressBLL();
                return _enderecoBLL;
            }
        }
        #endregion
        public ActionResult Get()
        {
            return View(FornecedorBLL.Get(new FornecedorModel(), user));
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

                if (FornecedorBLL.Insert(fornecedor, user))
                {
                    ViewBag.Sucesso = true;
                    return View("Get", FornecedorBLL.Get(new FornecedorModel(), user));
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
                if (FornecedorBLL.Update(fornecedor, user))
                    return View("Get", FornecedorBLL.Get(new FornecedorModel(), user));
                return View("Get", FornecedorBLL.Get(new FornecedorModel(), user));
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
                var result = FornecedorBLL.Get(new FornecedorModel() { Id = id }, user).FirstOrDefault();
                result.Endereco = EnderecoBLL.GetStates(result.Endereco);

                return View("Cadastrar", result);
            }
            catch (Exception)
            {
                return View("~/Views/Shared/Error.cshtml");
            }
        }
        private void LoadModel(ref FornecedorModel fornecedor)
        {
            fornecedor.Contato = new ContatoModel();
            fornecedor.Endereco = EnderecoBLL.GetStates(new EnderecoModel());
        }
    }
}
