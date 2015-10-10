using HELPER;
using HermesBarWCF;
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
    [HmaAuthorize(new int[] { (int)PerfilAuthorize.Perfil.Administrador })]
    public class FuncionarioController : Controller
    {
        private EmployeeService EmployeeService = Singleton<EmployeeService>.Instance();
        private AddressService AddressService = Singleton<AddressService>.Instance();
       
        private UsuarioModel user;
        public FuncionarioController()
        {
            GetSession.GetUserSession(ref this.user);
        }
        
        public ActionResult Get()
        {
            try
            {
                return View(EmployeeService.Get(new EmployeeModel() { Cpf = "" }, user));
            }
            catch (Exception)
            {
                return View("Error");
            }
        }
        public ActionResult Cadastrar()
        {
            try
            {
                var model = new EmployeeModel();
                LoadModel(ref model);
                return View(model);
            }
            catch (Exception)
            {
                return View("Error");
            }
        }
        public ActionResult CadastrarFuncionario(EmployeeModel employee, EnderecoModel address, ContatoModel contact)
        {
            try
            {
                employee.Contato = contact;
                employee.Endereco = address;

                if (employee.Id != 0)
                    return EditarFuncionar(employee);

                if (EmployeeService.Insert(employee, user))
                {
                    ViewBag.InsertSuccess = true;
                    return View("Get", EmployeeService.Get(new EmployeeModel() { Cpf = "" }, user));
                }
                ViewBag.InsertError = true;
                LoadModel(ref employee);
                return View("Cadastrar", employee);
            }
            catch (Exception ex)
            {
                ViewBag.CpfCadastrado = ex.Message;
                LoadModel(ref employee);
                return View("Cadastrar", employee);
            }
        }
        public ActionResult GetId(int id)
        {
            try
            {
                var model = EmployeeService.Get(new EmployeeModel() { Id = id, Cpf = "" }, user).FirstOrDefault();
                LoadModel(ref model);
                return View("Cadastrar", model);
            }
            catch (Exception)
            {
                return View("Error");
            }
        }
        private ActionResult EditarFuncionar(EmployeeModel employee)
        {
            try
            {
                if (EmployeeService.Update(employee, user))
                {
                    ViewBag.UpdateSuccess = true;
                    return View("Get", EmployeeService.Get(new EmployeeModel() { Cpf = "" }, user));
                }
                ViewBag.UpdateError = true;
                LoadModel(ref employee);
                return View("Cadastrar", employee);
            }
            catch (Exception ex)
            {
                ViewBag.CpfCadastrado = ex.Message;
                LoadModel(ref employee);
                return View("Cadastrar", employee);
            }
        }
        public ActionResult InactiveId(int id)
        {
            try
            {
                if (EmployeeService.Inactive(new EmployeeModel() { Id = id }, user))
                    ViewBag.InactiveSuccess = true;
                else
                    ViewBag.InactiveError = true;
                return View("Get", EmployeeService.Get(new EmployeeModel() { Cpf= "" }, user));
            }
            catch (Exception)
            {
                ViewBag.InactiveError = true;
                return View("Get", EmployeeService.Get(new EmployeeModel() { Cpf = "" }, user));
            }
        }
        public ActionResult ActiveId(int id)
        {
            try
            {
                if (EmployeeService.Active(new EmployeeModel() { Id = id }, user))
                    ViewBag.ActiveSuccess = true;
                else
                    ViewBag.ActiveError = true;
                return View("Get", EmployeeService.Get(new EmployeeModel() { Cpf = "" }, user));
            }
            catch (Exception)
            {
                ViewBag.ActiveError = true;
                return View("Get", EmployeeService.Get(new EmployeeModel() { Cpf = "" }, user));
            }
        }

        #region Private Methods
        private void LoadModel(ref EmployeeModel employee)
        {
            if(employee.Contato == null)
                employee.Contato = new MODEL.Contact.ContatoModel();
            if (employee.Endereco != null)
                employee.Endereco = AddressService.GetStates(employee.Endereco);
            else
                employee.Endereco = AddressService.GetStates(new EnderecoModel());
            
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

            var employeeType = EmployeeService.GetTypes();
            employee.Tipo = new List<SelectListItem>();
            foreach (var item in employeeType)
            {
                if (employee.TipoSelected == item.Id.ToString())
                    employee.Tipo.Add(new SelectListItem() { Text = item.Tipo, Value = item.Id.ToString(), Selected = true });
                else
                    employee.Tipo.Add(new SelectListItem() { Text = item.Tipo, Value = item.Id.ToString() });
            }

            var employeePlaces = EmployeeService.GetPlaces();
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
