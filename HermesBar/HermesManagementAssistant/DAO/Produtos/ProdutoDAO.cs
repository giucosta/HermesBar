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
                throw e;
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
            catch (Exception e)
            {
                Log.Log.GravarLog("PesquisaGrid", "ProdutoDAO", e.Message, Constantes.ATipoMetodo.SELECT);
                throw e;
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
                throw e;
	        }
        }
        public DataTable RecuperaProdutoEdicao(string codigoOriginal)
        {
            try
            {
                AccessObject<ProdutoModel> AO = new AccessObject<ProdutoModel>();
                AO.CreateSelectAll();
                AO.GetCommand();
                AO.InsertParameter(ConstantesDAO.WHERE, "CodigoOriginal", ConstantesDAO.EQUAL, codigoOriginal);

                return AO.GetDataTable();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public int RecuperaIdFornecedorProduto(ProdutoModel produto)
        {
            try
            {
                AccessObject<ProdutoModel> AO = new AccessObject<ProdutoModel>();
                AO.CreateSpecificQuery("SELECT Id_Fornecedor FROM Produto");
                AO.GetCommand();
                AO.InsertParameter(ConstantesDAO.WHERE, "Id_Produto", ConstantesDAO.EQUAL, produto.Id);

                return (int)AO.GetDataTable().Rows[0]["Id_Fornecedor"];
            }
            catch (Exception e)
            {
                Log.Log.GravarLog("RecuperaIdFornecedorProduto", "ProdutoDAO", e.Message, Constantes.ATipoMetodo.SELECT);
                throw e;
            }
        }
        public int RecuperaIdMarcaProduto(ProdutoModel produto)
        {
            try
            {
                AccessObject<ProdutoModel> AO = new AccessObject<ProdutoModel>();
                AO.CreateSpecificQuery("SELECT Id_Marca FROM Produto");
                AO.GetCommand();
                AO.InsertParameter(ConstantesDAO.WHERE, "Id_Produto", ConstantesDAO.EQUAL, produto.Id);

                return (int)AO.GetDataTable().Rows[0]["Id_Marca"];
            }
            catch (Exception e)
            {
                Log.Log.GravarLog("RecuperaIdMarcaProduto", "ProdutoDAO", e.Message, Constantes.ATipoMetodo.SELECT);
                throw e;
            }
        }
        public int RecuperaIdTipoProduto(ProdutoModel produto)
        {
            try
            {
                AccessObject<ProdutoModel> AO = new AccessObject<ProdutoModel>();
                AO.CreateSpecificQuery("SELECT Id_TipoProduto FROM Produto");
                AO.GetCommand();
                AO.InsertParameter(ConstantesDAO.WHERE, "Id_Produto", ConstantesDAO.EQUAL, produto.Id);

                return (int)AO.GetDataTable().Rows[0]["Id_TipoProduto"];
            }
            catch (Exception e)
            {
                Log.Log.GravarLog("RecuperaIdTipoProduto", "ProdutoDAO", e.Message, Constantes.ATipoMetodo.SELECT);
                throw e;
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
            catch (Exception e)
            {
                Log.Log.GravarLog("SugereProximoCodigo", "ProdutoDAO", e.Message, Constantes.ATipoMetodo.SELECT);
                throw e;
            }
        }
        public bool Editar(ProdutoModel produto)
        {
            try
            {
                AccessObject<ProdutoModel> AO = new AccessObject<ProdutoModel>();
                AO.CreateSpecificQuery("UPDATE Produto SET CodigoOriginal = @CodigoOriginal, Nome = @Nome, NomeReduzido = @NomeReduzido, Tipo = @Tipo, Marca = @Marca, Unidade = @Unidade, Fornecedor = @Fornecedor, QuantidadeEstoque = @QuantidadeEstoque, ValorCusto = @ValorCusto, ValorVenda = @ValorVenda, Observacao = @Observacao");
                AO.GetCommand();
                AO.InsertParameter("CodigoOriginal", produto.CodigoOriginal);
                AO.InsertParameter("Nome", produto.Nome);
                AO.InsertParameter("NomeReduzido", produto.NomeReduzido);
                AO.InsertParameter("Tipo",produto.Tipo.Id);
                AO.InsertParameter("Marca",produto.Marca.Id);
                AO.InsertParameter("Unidade",produto.Unidade);
                AO.InsertParameter("Fornecedor",produto.Fornecedor.Id);
                AO.InsertParameter("QuantidadeEstoque",produto.QuantidadeEstoque);
                AO.InsertParameter("ValorCusto",produto.ValorCusto);
                AO.InsertParameter("ValorVenda",produto.ValorVenda);
                AO.InsertParameter("Observacao",produto.Observacao);

                return AO.ExecuteCommand();
            }
            catch (Exception e)
            {
                Log.Log.GravarLog("Editar", "ProdutoDAO", e.Message, Constantes.ATipoMetodo.UPDATE);
                throw e;
            }
        }
    }

}
