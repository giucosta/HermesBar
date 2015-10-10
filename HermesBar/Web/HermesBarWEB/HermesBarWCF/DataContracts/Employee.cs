using BLL.Employee;
using HELPER;
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
        #region Singleton
        private EmployeeBLL EmployeeBLL;
        private TypeEmployeeBLL TypeEmployeeBLL;
        private PlaceEmployeeBLL PlaceEmployeeBLL;
        #endregion

        public Employee()
        {
            this.EmployeeBLL = Singleton<EmployeeBLL>.Instance();
            this.TypeEmployeeBLL = Singleton<TypeEmployeeBLL>.Instance();
            this.PlaceEmployeeBLL = Singleton<PlaceEmployeeBLL>.Instance();
        }
        public List<EmployeeModel> Get(EmployeeModel model, UsuarioModel user)
        {
            return EmployeeBLL.Get(model, user);
        }
        public bool Insert(EmployeeModel model, UsuarioModel user)
        {
            return EmployeeBLL.Insert(model, user);
        }
        public List<TypeEmployeeModel> GetTypes()
        {
            return TypeEmployeeBLL.Get();
        }
        public List<PlaceEmployeeModel> GetPlaces()
        {
            return PlaceEmployeeBLL.Get();
        }
        public bool Update(EmployeeModel model, UsuarioModel user)
        {
            return EmployeeBLL.Update(model, user);
        }
        public bool Active(EmployeeModel model, UsuarioModel user)
        {
            return EmployeeBLL.Active(model, user);
        }
        public bool Inactive(EmployeeModel model, UsuarioModel user)
        {
            return EmployeeBLL.Inactive(model, user);
        }
    }
}