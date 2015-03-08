using DAO.Connections;
using DAO.Utils;
using MODEL.Produto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTILS;

namespace DAO.Produtos
{
    public class ProdutoDAO
    {
        public bool Salvar(ProdutoModel produto)
        {
            try
            {
                AccessObject<ProdutoModel> AO = new AccessObject<ProdutoModel>();
                AO.CreateDataInsert();
                Connection.GetCommand(AO.ReturnQuery());
                Connection.AddParameter("@CodigoOriginal",produto.CodigoOriginal);
                Connection.AddParameter("@CodigoBarras",produto.CodigoBarras);
                Connection.AddParameter("@Nome",produto.Nome);
                Connection.AddParameter("@NomeReduzido",produto.NomeReduzido);
                Connection.AddParameter("@Id_TipoProduto",produto.Tipo.Id);
                Connection.AddParameter("@Marca",produto.Marca);
                Connection.AddParameter("@Unidade",produto.Unidade);
                Connection.AddParameter("@Id_Fornecedor",produto.Fornecedor.Id);
                Connection.AddParameter("@QuantidadeEstoque",produto.QuantidadeEstoque);
                Connection.AddParameter("@EstoqueMinimo",produto.EstoqueMinimo);
                Connection.AddParameter("@EstoqueIdeal", produto.EstoqueIdeal);
                Connection.AddParameter("@ValorCusto",produto.ValorCusto);
                Connection.AddParameter("@ValorVenda", produto.ValorVenda);
                Connection.AddParameter("@Observacao",produto.Observacao);

                return Connection.ExecutarComando();
            }
            catch (Exception e)
            {
                Log.Log.GravarLog("Salvar","ProdutoDAO",e.StackTrace,Constantes.ATipoMetodo.INSERT);
                return false;
            }
        }
        public DataTable PesquisarProdutoCodigo(ProdutoModel produto)
        {
            try
            {
                AccessObject<ProdutoModel> AO = new AccessObject<ProdutoModel>();
                AO.CreateSelectAll();
                AO.InsertParameter(ConstantesDAO.WHERE, "CodigoOriginal", ConstantesDAO.EQUAL, "@CodigoOriginal");
                Connection.GetCommand(AO.ReturnQuery());
                Connection.AddParameter("@CodigoOriginal",produto.CodigoOriginal);
                return Connection.getDataTable();
            }
            catch (Exception e)
            {
                Log.Log.GravarLog("PesquisarProdutoCodigo", "ProdutoDAO", e.StackTrace, Constantes.ATipoMetodo.SELECT);
                return null;
            }
        }
        public DataTable PesquisarProdutoNomeReduzido(ProdutoModel produto)
        {
            try
            {
                AccessObject<ProdutoModel> AO = new AccessObject<ProdutoModel>();
                AO.CreateSelectAll();
                AO.InsertParameter(ConstantesDAO.WHERE, "NomeReduzido", ConstantesDAO.LIKE, "@NomeReduzido");
                Connection.GetCommand(AO.ReturnQuery());
                Connection.AddParameter("@NomeReduzido", produto.NomeReduzido);
                return Connection.getDataTable();
            }
            catch (Exception e)
            {
                Log.Log.GravarLog("PesquisarProdutoNomeReduzido", "ProdutoDAO", e.StackTrace, Constantes.ATipoMetodo.SELECT);
                return null;
            }
        }
        public DataTable RetornaProdutos()
        {
            try
            {
                AccessObject<ProdutoModel> AO = new AccessObject<ProdutoModel>();
                AO.CreateSelectAll();
                Connection.GetCommand(AO.ReturnQuery());
                return Connection.getDataTable();
            }
            catch (Exception e)
            {
                Log.Log.GravarLog("RetornaProdutos","ProdutoDAO",e.StackTrace,Constantes.ATipoMetodo.SELECT);
                return null;
            }
        }
    }
}
