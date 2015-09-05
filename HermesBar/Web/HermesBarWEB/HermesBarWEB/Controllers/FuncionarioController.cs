using HermesBarWEB.UTIL;
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
        private UsuarioModel user;
        public FuncionarioController()
        {
            GetSession.GetUserSession(ref this.user);
        }
        public ActionResult Get()
        {
            return View(Service.Get(new EmployeeModel(), user));
        }
    }
}
