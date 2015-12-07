using DAO.Produtos;
using MODEL.Produto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTIL;

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
            try
            {
                return TipoProdutoDAO.Salvar(tipoProduto);
            }
            catch (Exception e)
            {
                UTIL.Session.MensagemErro = e.Message;
                return false;
            }
        }
        public bool Excluir(TipoProdutoModel tipoProduto)
        {
            try
            {
                return TipoProdutoDAO.Excluir(tipoProduto);
            }
            catch (Exception e)
            {
                UTIL.Session.MensagemErro = e.Message;
                return false;
            }
        }
        public List<TipoProdutoModel> Pesquisa(TipoProdutoModel tipoProduto)
        {
            try
            {
                return TipoProdutoDAO.Pesquisa(tipoProduto).DataTableToList<TipoProdutoModel>();
            }
            catch (Exception e)
            {
                UTIL.Session.MensagemErro = e.Message;
                return null;
            }
        }
        public bool Editar(TipoProdutoModel tipo)
        {
            try
            {
                return TipoProdutoDAO.Editar(tipo);
            }
            catch (Exception e)
            {
                UTIL.Session.MensagemErro = e.Message;
                return false;
            }
        }
        public List<TipoProdutoModel> RetornaTipos()
        {
            try
            {
                return TipoProdutoDAO.RetornaTipos().DataTableToList<TipoProdutoModel>();
            }
            catch (Exception e)
            {
                UTIL.Session.MensagemErro = e.Message;
                return null;
            }
        }
        public TipoProdutoModel RecuperaTipoId(int id)
        {
            try
            {
                return TipoProdutoDAO.RecuperaTipoId(id).DataTableToSimpleObject<TipoProdutoModel>();
            }
            catch (Exception e)
            {
                UTIL.Session.MensagemErro = e.Message;
                return null;
            }
        }
    }
}
