using BLL.Product;
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
        private ProductBLL _produtoBLL = null;
        private ProductBLL ProdutoBLL
        {
            get
            {
                if (_produtoBLL == null)
                    _produtoBLL = new ProductBLL();
                return _produtoBLL;
            }
        }

        public bool Insert(ProdutoModel produto, UsuarioModel usuario)
        {
            return ProdutoBLL.Insert(produto, usuario);
        }
        public List<ProdutoModel> Get()
        {
            return ProdutoBLL.Get();
        }
        public ProdutoModel GetId(int id, int ativo)
        {
            return ProdutoBLL.GetId(id, ativo);
        }
        public bool Active(ProdutoModel produto, UsuarioModel usuario)
        {
            return ProdutoBLL.Active(produto, usuario);
        }
        public bool Inactive(ProdutoModel produto, UsuarioModel usuario)
        {
            return ProdutoBLL.Inactive(produto, usuario);
        }
        public int GetNextCode()
        {
            return ProdutoBLL.GetNextCode();
        }
        public bool Update(ProdutoModel produto, UsuarioModel usuario)
        {
            return ProdutoBLL.Update(produto, usuario);
        }
    }
}