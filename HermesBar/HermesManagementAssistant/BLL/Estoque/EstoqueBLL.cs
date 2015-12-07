using BLL.Produtos;
using DAO.Estoque;
using MODEL.Estoque;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Estoque
{
    public class EstoqueBLL
    {
        private EstoqueDAO _estoqueDAO = null;
        public EstoqueDAO EstoqueDAO
        {
            get
            {
                if (_estoqueDAO == null)
                    _estoqueDAO = new EstoqueDAO();
                return _estoqueDAO;
            }
        }
        private ProdutoBLL _produtoBLL = null;
        public ProdutoBLL ProdutoBLL
        {
            get
            {
                if (_produtoBLL == null)
                    _produtoBLL = new ProdutoBLL();
                return _produtoBLL;
            }
        }

        public bool Salvar(EstoqueModel estoque)
        {
            try
            {
                if (!string.IsNullOrEmpty(estoque.Produto.CodigoOriginal))
                {
                    estoque.Produto = ProdutoBLL.PesquisaProdutoCodigo(estoque.Produto).First();
                    return EstoqueDAO.Salvar(estoque);
                }
            }
            catch (Exception e)
            {
                UTIL.Session.MensagemErro = e.Message;
            }
            return false;
        }
        public bool Editar(EstoqueModel estoque)
        {
            try
            {   
                if (!string.IsNullOrEmpty(estoque.Produto.CodigoOriginal))
                {
                    estoque.Produto = ProdutoBLL.PesquisaProdutoCodigo(estoque.Produto).First();
                    return EstoqueDAO.Editar(estoque);
                }
            }
            catch (Exception e)
            {
                UTIL.Session.MensagemErro = e.Message;
            }
            return false;
        }
    }
}
