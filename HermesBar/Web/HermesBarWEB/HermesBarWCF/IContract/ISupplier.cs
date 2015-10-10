using MODEL.Supplier;
using MODEL.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;

namespace HermesBarWCF.IContract
{
    [ServiceContract]
    public interface ISupplier
    {
        bool Insert(FornecedorModel supplier, UsuarioModel user);
        List<FornecedorModel> Get(FornecedorModel model, UsuarioModel user);
        bool Update(FornecedorModel supplier, UsuarioModel user);
    }
}