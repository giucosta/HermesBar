using BLL.Employee;
using MODEL.Employee;
using MODEL.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace HermesBarWCF.DataContracts
{
    [DataContract]
    public class Employee
    {
        private EmployeeBLL _employeeBLL = null;
        private EmployeeBLL EmployeeBLL
        {
            get
            {
                if (_employeeBLL == null)
                    _employeeBLL = new EmployeeBLL();
                return _employeeBLL;
            }
        }

        public List<EmployeeModel> Get(EmployeeModel model, UsuarioModel user)
        {
            return EmployeeBLL.Get(model, user);
        }
    }
}