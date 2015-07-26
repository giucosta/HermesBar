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
            //ViewBag.Email = GetEmail.Get();
        }

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

        public ActionResult Get()
        {
            return View(FornecedorBLL.Get(new FornecedorModel(), user));
        }
        public ActionResult Cadastrar()
        {
            var model = new FornecedorModel()
            {
                Contato = new ContatoModel(),
                Endereco = new EnderecoModel()
            };
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

                FornecedorBLL.Insert(fornecedor, user);
                return View("Cadastrar");
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
                
                throw;
            }
        }
        public ActionResult GetId(int id)
        {
            try
            {
                var model = new FornecedorModel() { Id = id };
                return View("Cadastrar", FornecedorBLL.Get(model, user).FirstOrDefault());
            }
            catch (Exception)
            {
                return View("~/Views/Shared/Error.cshtml");
            }
        }
    }
}
