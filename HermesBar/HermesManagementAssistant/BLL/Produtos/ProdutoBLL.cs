using DAO.Produtos;
using MODEL.Produto;
using System;
using System.Collections.Generic;
using System.Data;
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
        private TipoProdutoBLL _tipoProdutoBLL = null;
        public TipoProdutoBLL TipoProdutoBLL
        {
            get
            {
                if (_tipoProdutoBLL == null)
                    _tipoProdutoBLL = new TipoProdutoBLL();
                return _tipoProdutoBLL;
            }
        }
        public bool Salvar(ProdutoModel produto)
        {
            return ProdutoDAO.Salvar(produto);
        }
        public DataTable Pesquisa(ProdutoModel produto)
        {
            if (produto.Tipo != null) 
                produto.Tipo = (TipoProdutoModel)TipoProdutoBLL.Pesquisa(produto.Tipo).Where(x => x.Tipo == produto.Tipo.Tipo);

            return ProdutoDAO.PesquisaGrid(produto);
        }
    }
}
