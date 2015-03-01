using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO.Abstract;
using MODEL.Produtos;
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
                //var sql = "INSERT INTO TipoProduto VALUES @Tipo, @Descricao";
                Connection.GetCommand(AO.ReturnQuery());
                Connection.AddParameter("@Tipo", tipoProduto.Tipo);
                Connection.AddParameter("@Descricao", tipoProduto.Descricao);

                return Connection.ExecutarComando();
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
                Connection.GetCommand(AO.ReturnQuery());
                Connection.AddParameter("@Tipo",tipoProduto.Tipo);

                return Connection.ExecutarComando();
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
                var sql = @"SELECT * FROM TipoProduto WHERE Tipo LIKE @Tipo OR Descricao LIKE @Descricao";
                var comando = new SqlCommand(sql, Connection.GetConnection());
                comando.Parameters.AddWithValue("@Tipo", "%" + tipoProduto.Tipo + "%");
                comando.Parameters.AddWithValue("@Descricao", "%" + tipoProduto.Descricao + "%");

                var dataTable = Connection.getDataTable(comando);
                var tipoProdutoTabela = CriaTabelaTipoProduto();

                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    tipoProdutoTabela.Rows.Add(
                        dataTable.Rows[i]["Id_TipoProduto"],
                        dataTable.Rows[i]["Tipo"],
                        dataTable.Rows[i]["Descricao"]
                    );
                }
                return tipoProdutoTabela;
            }
            catch (Exception e)
            {
                Log.Log.GravarLog("Pesquisar","TipoProdutoDAO",e.StackTrace,Constantes.ATipoMetodo.SELECT);
                return null;
            }
        }
        public DataTable CriaTabelaTipoProduto()
        {
            var dataTable = new DataTable();
            dataTable.Columns.Add("Id",typeof(int));
            dataTable.Columns.Add("Tipo", typeof(string));
            dataTable.Columns.Add("Descrição", typeof(string));

            return dataTable;
        }
    }
}
