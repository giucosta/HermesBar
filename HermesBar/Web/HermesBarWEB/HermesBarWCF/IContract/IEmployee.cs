using MODEL.Employee;
using MODEL.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;

namespace HermesBarWCF.IContract
{
    [ServiceContract]
    public interface IEmployee
    {
        List<EmployeeModel> Get(EmployeeModel model, UsuarioModel user);
        bool Insert(EmployeeModel model, UsuarioModel user);
        bool Update(EmployeeModel model, UsuarioModel user);
        List<TypeEmployeeModel> GetTypes();
        List<PlaceEmployeeModel> GetPlaces();
    }
}