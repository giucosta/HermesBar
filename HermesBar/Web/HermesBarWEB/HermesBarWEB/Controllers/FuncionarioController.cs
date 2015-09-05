using HermesBarWEB.UTIL;
using MODEL.Address;
using MODEL.Contact;
using MODEL.Employee;
using MODEL.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HermesBarWEB.Controllers
{
    public class FuncionarioController : Controller
    {
        private HermesBarWCF.FuncionarioService _service = null;
        private HermesBarWCF.FuncionarioService Service
        {
            get
            {
                if (_service == null)
                    _service = new HermesBarWCF.FuncionarioService();
                return _service;
            }
        }
        private HermesBarWCF.EnderecoService _enderecoService = null;
        private HermesBarWCF.EnderecoService EnderecoService
        {
            get
            {
                if (_enderecoService == null)
                    _enderecoService = new HermesBarWCF.EnderecoService();
                return _enderecoService;
            }
        }
        
        private UsuarioModel user;
        public FuncionarioController()
        {
            GetSession.GetUserSession(ref this.user);
        }

        public ActionResult Get()
        {
            return View(Service.Get(new EmployeeModel(), user));
        }
        public ActionResult Cadastrar()
        {
            var model = new EmployeeModel();
            LoadModel(ref model);
            return View(model);
        }
        public ActionResult CadastrarFuncionario(EmployeeModel employee, EnderecoModel address, ContatoModel contact)
        {
            try
            {
                employee.Contato = contact;
                employee.Endereco = address;

                if (Service.Insert(employee, user))
                    return View("Get", Service.Get(new EmployeeModel(), user));
                return View("Get", Service.Get(new EmployeeModel(), user));
            }
            catch (Exception)
            {
                throw;
            }
        }

        #region Private Methods
        private void LoadModel(ref EmployeeModel employee)
        {
            employee.Contato = new MODEL.Contact.ContatoModel();
            employee.Endereco = EnderecoService.GetStates(new EnderecoModel());
            
            employee.Status = new List<SelectListItem>();
            foreach (var item in Enum.GetValues(typeof(Enumerators.Status)))
            {
                if (employee.StatusSelected == ((int)item).ToString())
                    employee.Status.Add(new SelectListItem() { Text = item.ToString(), Value = ((int)item).ToString(), Selected = true });
                else
                    employee.Status.Add(new SelectListItem() { Text = item.ToString(), Value = ((int)item).ToString() });
            }

            employee.Sexo = new List<SelectListItem>();
            foreach (var item in Enum.GetValues(typeof(Enumerators.Sexo)))
            {
                if (employee.SexoSelected == ((int)item).ToString())
                    employee.Sexo.Add(new SelectListItem() { Text = item.ToString(), Value = ((int)item).ToString(), Selected = true });
                else
                    employee.Sexo.Add(new SelectListItem() { Text = item.ToString(), Value = ((int)item).ToString() });
            }

            var employeeType = Service.GetTypes();
            employee.Tipo = new List<SelectListItem>();
            foreach (var item in employeeType)
            {
                if (employee.TipoSelected == item.Id.ToString())
                    employee.Tipo.Add(new SelectListItem() { Text = item.Tipo, Value = item.Id.ToString(), Selected = true });
                else
                    employee.Tipo.Add(new SelectListItem() { Text = item.Tipo, Value = item.Id.ToString() });
            }

            var employeePlaces = Service.GetPlaces();
            employee.Cargo = new List<SelectListItem>();
            foreach (var item in employeePlaces)
            {
                if (employee.CargoSelected == item.Id.ToString())
                    employee.Cargo.Add(new SelectListItem() { Text = item.Cargo, Value = item.Id.ToString(), Selected = true });
                else
                    employee.Cargo.Add(new SelectListItem() { Text = item.Cargo, Value = item.Id.ToString() });
            }
        }
        #endregion
    }
}
