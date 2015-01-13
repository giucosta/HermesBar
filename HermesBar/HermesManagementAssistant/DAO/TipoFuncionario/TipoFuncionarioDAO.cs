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

namespace DAO.TipoFuncionario
{
    public class TipoFuncionarioDAO
    {
        public TipoFuncionarioModel Salvar(TipoFuncionarioModel tipoFuncionario)
        {
            try
            {
                var sql = @"INSERT INTO TipoFuncionario VALUES (@TipoFuncionario)";
                var comando = new SqlCommand(sql, Connection.GetConnection());
                comando.Parameters.AddWithValue("@TipoFuncionario", tipoFuncionario.TipoFuncionario);

                Connection.ExecutarComando(comando);
                return RetornaTipo(tipoFuncionario);
            }
            catch (Exception e)
            {
                Log.Log.GravarLog("Salvar", "TipoFuncionarioDAO", e.StackTrace, Constantes.ATipoMetodo.INSERT);
                return null;
            }
        }
        public TipoFuncionarioModel RetornaTipo(TipoFuncionarioModel tipoFuncionario)
        {
            try
            {
                var sql = "SELECT * FROM TipoFuncionario WHERE Tipo = @TipoFuncionario";
                var comando = new SqlCommand(sql, Connection.GetConnection());
                comando.Parameters.AddWithValue("@TipoFuncionario", tipoFuncionario.TipoFuncionario);

                return CarregaTipoFuncionario(Connection.getDataTable(comando));
            }
            catch (Exception e)
            {
                Log.Log.GravarLog("RetornaTipo", "TipoFuncionarioDAO", e.StackTrace, Constantes.ATipoMetodo.SELECT);
                return null;
            }
        }
        private TipoFuncionarioModel CarregaTipoFuncionario(DataTable data)
        {
            try
            {
                if (data.Rows.Count != 0)
                {
                    var tipo = new TipoFuncionarioModel();
                    tipo.Id = (int)data.Rows[0]["Id_TipoFuncionario"];
                    tipo.TipoFuncionario = data.Rows[0]["Tipo"].ToString();

                    return tipo;
                }
                else
                    return null;
            }
            catch (Exception)
            {
                return null;
            }   
        }
    }
}
