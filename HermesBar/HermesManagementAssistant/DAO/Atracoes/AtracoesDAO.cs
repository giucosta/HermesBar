using DAO.Abstract;
using DAO.Connections;
using MODEL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTILS;

namespace DAO.Atracoes
{
    public class AtracoesDAO : IAtracoes
    {
        public bool Salvar(AtracoesModel atracoes)
        {
            try
            {
                var sql = @"INSERT INTO Atracoes VALUES (@Nome, @Estilo)";
                var comando = new SqlCommand(sql, Connection.GetConnection());
                comando.Parameters.AddWithValue("@Nome",atracoes.Nome);
                comando.Parameters.AddWithValue("@Estilo", atracoes.EstiloPredominante);

                Connection.ExecutarComando(comando);
                return true;
            }
            catch (Exception e)
            {
                Log.Log.GravarLog("Salvar", "AtracoesDAO", e.StackTrace, Constantes.ATipoMetodo.INSERT);
                return false;
            }
        }

        public bool Excluir(AtracoesModel atracoes)
        {
            try
            {
                var sql = @"DELETE FROM Atracoes WHERE Id_Atracoes = @Id";
                var comando = new SqlCommand(sql,Connection.GetConnection());
                comando.Parameters.AddWithValue("@Id",atracoes.Id);
                Connection.ExecutarComando(comando);
                return true;
            }
            catch (Exception e)
            {
                Log.Log.GravarLog("Excluir", "ExcluirDAO", e.StackTrace, Constantes.ATipoMetodo.DELETE);
                return false;
            }
        }

        public DataTable Pesquisa(AtracoesModel atracoes)
        {
            try
            {
                var sql = @"SELECT * FROM Atracoes 
                            WHERE 1=1 
                            AND Nome LIKE @Nome
                            AND Estilo LIKE @Estilo";
                var comando = new SqlCommand(sql,Connection.GetConnection());

                comando.Parameters.AddWithValue("@Nome", "%" + atracoes.Nome + "%");
                comando.Parameters.AddWithValue("@Estilo", "%" + atracoes.EstiloPredominante + "%");

                return Connection.getDataTable(comando);
            }
            catch (Exception e)
            {
                Log.Log.GravarLog("Pesquisa","AtracoesDAO",e.StackTrace,Constantes.ATipoMetodo.SELECT);
                return null;
            }
        }
    }
}
