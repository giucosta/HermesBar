using BLL.Product;
using HELPER;
using MODEL.Product;
using MODEL.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace HermesBarWCF.DataContracts
{
    [DataContract]
    public class Produtos
    {
        private ProductBLL ProdutoBLL;

        public Produtos()
        {
            this.ProdutoBLL = Singleton<ProductBLL>.Instance();
        }
        public bool Insert(ProdutoModel product, UsuarioModel user)
        {
            return ProdutoBLL.Insert(product, user);
        }
        public List<ProdutoModel> Get()
        {
            return ProdutoBLL.Get();
        }
        public ProdutoModel GetId(int id, int ativo)
        {
            return ProdutoBLL.GetId(id, ativo);
        }
        public bool Active(ProdutoModel product, UsuarioModel user)
        {
            return ProdutoBLL.Active(product, user);
        }
        public bool Inactive(ProdutoModel product, UsuarioModel user)
        {
            return ProdutoBLL.Inactive(product, user);
        }
        public int GetNextCode()
        {
            return ProdutoBLL.GetNextCode();
        }
        public bool Update(ProdutoModel product, UsuarioModel user)
        {
            return ProdutoBLL.Update(product, user);
        }
    }
}