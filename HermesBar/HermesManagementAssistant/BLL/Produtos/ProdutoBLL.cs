using DAO.Produtos;
using MODEL.Produto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTILS;

namespace BLL.Produtos
{
    public class ProdutoBLL
    {
        private ProdutoDAO _produtoDAO = null;
        public ProdutoDAO ProdutoDAO
        {
            get
            {
                if (_produtoDAO == null)
                    _produtoDAO = new ProdutoDAO();
                return _produtoDAO;
            }
        }
        public bool Salvar(ProdutoModel produto)
        {
            return ProdutoDAO.Salvar(produto);
        }
        public List<ProdutoModel> RetornaProdutos()
        {
            return ProdutoDAO.RetornaProdutos().DataTableToList<ProdutoModel>();
        }
    }
}
