using DAO.Connections;
using DAO.Utils;
using MODEL.Produto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTIL;

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
                AO.GetCommand();
                AO.InsertParameter("CodigoOriginal", produto.CodigoOriginal);
                AO.InsertParameter("CodigoBarras", produto.CodigoBarras);
                AO.InsertParameter("Nome", produto.Nome);
                AO.InsertParameter("NomeReduzido", produto.NomeReduzido);
                AO.InsertParameter("Tipo", produto.Tipo.Id);
                AO.InsertParameter("Marca", produto.Marca.Id);
                AO.InsertParameter("Unidade", produto.Unidade);
                AO.InsertParameter("Fornecedor", produto.Fornecedor.Id);
                AO.InsertParameter("QuantidadeEstoque", produto.QuantidadeEstoque);
                AO.InsertParameter("EstoqueMinimo", produto.EstoqueMinimo);
                AO.InsertParameter("EstoqueIdeal", produto.EstoqueIdeal);
                AO.InsertParameter("ValorCusto", produto.ValorCusto);
                AO.InsertParameter("ValorVenda", produto.ValorVenda);
                AO.InsertParameter("Observacao", produto.Observacao);
                
                return Connection.ExecutarComando();
            }
            catch (Exception e)
            {
                Log.Log.GravarLog("Salvar","ProdutoDAO",e.StackTrace,Constantes.ATipoMetodo.INSERT);
                return false;
            }
        }
        public DataTable PesquisaGrid(ProdutoModel produto)
        {
            try
            {
                AccessObject<ProdutoModel> AO = new AccessObject<ProdutoModel>();
                AO.CreateSpecificQuery("SELECT Produto.CodigoOriginal, Produto.Nome, Marca.Marca, Produto.Unidade, Produto.QuantidadeEstoque, Produto.ValorVenda, TipoProduto.Tipo FROM Produto");
                AO.GetCommand();
                AO.CreateInnerJoin("TipoProduto", "Id_TipoProduto");
                AO.CreateInnerJoin("Marca", "Id_Marca");
                AO.InsertParameter(ConstantesDAO.WHERE, "Produto.Nome", ConstantesDAO.LIKE, produto.Nome);
                AO.InsertParameter(ConstantesDAO.AND, "Produto.CodigoOriginal", ConstantesDAO.LIKE, produto.CodigoOriginal);
                if(produto.Tipo != null)
                    AO.InsertParameter(ConstantesDAO.AND, "TipoProduto.Tipo", ConstantesDAO.LIKE, produto.Tipo.Tipo);

                return AO.GetDataTable();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataTable PesquisaProdutoCodigo(ProdutoModel produto)
        {
            try 
	        {
                AccessObject<ProdutoModel> AO = new AccessObject<ProdutoModel>();
                AO.CreateSelectAll();
                AO.GetCommand();
                AO.InsertParameter(ConstantesDAO.WHERE, "CodigoOriginal", ConstantesDAO.EQUAL, produto.CodigoOriginal);
                return AO.GetDataTable();
	        }
	        catch (Exception e)
	        {
                Log.Log.GravarLog("PesquisaProdutoCodigo", "ProdutoDAO", e.Message, Constantes.ATipoMetodo.SELECT);
                return null;
	        }
        }
        public DataTable SugereProximoCodigo()
        {
            try
            {
                AccessObject<ProdutoModel> AO = new AccessObject<ProdutoModel>();
                AO.CreateSpecificQuery("SELECT MAX(CodigoOriginal) As CodigoOriginal FROM Produto");
                AO.GetCommand();
                return AO.GetDataTable();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

}
