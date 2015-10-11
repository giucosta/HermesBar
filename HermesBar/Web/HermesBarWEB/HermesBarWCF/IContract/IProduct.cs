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
        bool Insert(ProdutoModel product, UsuarioModel user);
        List<ProdutoModel> Get();
        ProdutoModel GetId(int id, int ativo);
        bool Active(ProdutoModel product, UsuarioModel user);
        bool Inactive(ProdutoModel product, UsuarioModel user);
        int GetNextCode();
        bool Update(ProdutoModel product, UsuarioModel user);
        List<ProdutoModel> GetLow();
    }
}