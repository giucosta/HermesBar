using MODEL.Product;
using MODEL.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;

namespace HermesBarWCF.IContract
{
    [ServiceContract]
    public interface IProductType
    {
        List<TipoModel> Get();
        TipoModel GetId(TipoModel tipo, UsuarioModel user);
        bool Insert(TipoModel tipo, UsuarioModel user);
        bool Update(TipoModel tipo, UsuarioModel user);
    }
}