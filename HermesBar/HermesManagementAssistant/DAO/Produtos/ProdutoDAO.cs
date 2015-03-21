﻿using DAO.Connections;
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
        public DataTable PesquisaGrid(ProdutoModel produto)
        {
            try
            {
                AccessObject<ProdutoModel> AO = new AccessObject<ProdutoModel>();
                AO.CreateSpecificQuery("SELECT Produto.CodigoOriginal, Produto.Nome, Produto.Marca, Produto.Unidade, Produto.QuantidadeEstoque, Produto.ValorVenda, TipoProduto.Tipo FROM Produto");
                AO.GetCommand();
                AO.CreateInnerJoin("TipoProduto", "Id_TipoProduto");
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
    }

}
