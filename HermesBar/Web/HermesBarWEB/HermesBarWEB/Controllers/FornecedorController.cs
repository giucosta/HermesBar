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

namespace HermesBarWEB.Controllers
{
    public class FornecedorController : Controller
    {
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
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Cadastrar(FornecedorModel fornecedor, EnderecoModel endereco, ContatoModel contato)
        {
            fornecedor.Contato = contato;
            fornecedor.Endereco = endereco;

            FornecedorBLL.Insert(fornecedor, (UsuarioModel)Session["USR"]);
            return View("Index");
        }
    }
}
