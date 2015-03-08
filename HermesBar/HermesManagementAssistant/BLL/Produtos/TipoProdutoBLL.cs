using DAO.Produtos;
using MODEL.Produto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Produtos
{
    public class TipoProdutoBLL
    {
        private TipoProdutoDAO _tipoProdutoDAO = null;
        public TipoProdutoDAO TipoProdutoDAO
        {
            get
            {
                if (_tipoProdutoDAO == null)
                    _tipoProdutoDAO = new TipoProdutoDAO();
                return _tipoProdutoDAO;
            }
        }

        public bool Salvar(TipoProdutoModel tipoProduto)
        {
            return TipoProdutoDAO.Salvar(tipoProduto);
        }
        public bool Excluir(TipoProdutoModel tipoProduto)
        {
            return TipoProdutoDAO.Excluir(tipoProduto);
        }
        public DataTable Pesquisa(TipoProdutoModel tipoProduto)
        {
            return TipoProdutoDAO.Pesquisa(tipoProduto);
        }
    }
}
