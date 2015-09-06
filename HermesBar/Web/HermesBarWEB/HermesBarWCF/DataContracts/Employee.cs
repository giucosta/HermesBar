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
        #region Singleton
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
        private TypeEmployeeBLL _typeEmployee = null;
        private TypeEmployeeBLL TypeEmployeeBLL
        {
            get
            {
                if (_typeEmployee == null)
                    _typeEmployee = new TypeEmployeeBLL();
                return _typeEmployee;
            }
        }
        private PlaceEmployeeBLL _placeEmployee = null;
        private PlaceEmployeeBLL PlaceEmployeeBLL
        {
            get
            {
                if (_placeEmployee == null)
                    _placeEmployee = new PlaceEmployeeBLL();
                return _placeEmployee;
            }
        }
        #endregion

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
    }
}