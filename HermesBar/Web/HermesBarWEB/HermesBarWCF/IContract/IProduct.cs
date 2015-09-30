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
    public interface IProduct
    {
        bool Insert(ProdutoModel produto, UsuarioModel usuario);
        List<ProdutoModel> Get();
        ProdutoModel GetId(int id, int ativo);
        bool Active(ProdutoModel produto, UsuarioModel usuario);
        bool Inactive(ProdutoModel produto, UsuarioModel usuario);
        int GetNextCode();
        bool Update(ProdutoModel produto, UsuarioModel usuario);
    }
}