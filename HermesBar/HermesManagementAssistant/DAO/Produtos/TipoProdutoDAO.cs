using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO.Abstract;
using MODEL.Produto;
using DAO;
using System.Data.SqlClient;
using DAO.Connections;
using System.Data;
using UTILS;
using DAO.Utils;

namespace DAO.Produtos
{
    public class TipoProdutoDAO : IDAO<TipoProdutoModel>
    {
        public bool Salvar(TipoProdutoModel tipoProduto)
        {
            try
            {
                AccessObject<TipoProdutoModel> AO = new AccessObject<TipoProdutoModel>();
                AO.CreateDataInsert();
                AO.GetCommand();
                AO.InsertParameter("Tipo", tipoProduto.Tipo);
                AO.InsertParameter("Descricao", tipoProduto.Descricao);

                return AO.ExecuteCommand();
            }
            catch (Exception e)
            {
                Log.Log.GravarLog("Salvar","TipoProdutoDAO",e.StackTrace , Constantes.ATipoMetodo.INSERT);
                return false;
            }
        }
        public bool Excluir(TipoProdutoModel tipoProduto)
        {
            try
            {
                AccessObject<TipoProdutoModel> AO = new AccessObject<TipoProdutoModel>();
                AO.CreateSpecificQuery(@"DELETE TipoProduto WHERE Tipo = @Tipo");
                AO.GetCommand();
                AO.InsertParameter("Tipo", tipoProduto.Tipo);

                return AO.ExecuteCommand();
            }
            catch (Exception e)
            {
                Log.Log.GravarLog("Excluir","TipoProdutoDAO",e.StackTrace, Constantes.ATipoMetodo.DELETE);
                return false;
            }
        }
        public DataTable Pesquisa(TipoProdutoModel tipoProduto)
        {
            try
            {
                AccessObject<TipoProdutoModel> AO = new AccessObject<TipoProdutoModel>();
                AO.CreateSelectAll();
                AO.GetCommand();
                AO.InsertParameter(ConstantesDAO.WHERE, "Tipo", ConstantesDAO.LIKE, tipoProduto.Tipo);

                return AO.GetDataTable();
            }
            catch (Exception e)
            {
                Log.Log.GravarLog("Pesquisar","TipoProdutoDAO",e.StackTrace,Constantes.ATipoMetodo.SELECT);
                return null;
            }
        }
    }
}
