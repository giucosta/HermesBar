using DAO.Connections;
using MODEL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO;

namespace DAO.Funcionario
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

        public List<String> TiposFuncionarios()
        {
            try
            {
                var sql = @"SELECT Tipo FROM TipoFuncionario";
                var comando = new SqlCommand(sql, Connection.GetConnection());
                var dataTable = Connection.getDataTable(comando);
                var listaTipos = new List<String>();

                for (int i = 0; i < dataTable.Rows.Count; i++)
                    listaTipos.Add(dataTable.Rows[i]["Tipo"].ToString());

                return listaTipos;
            }
            catch (Exception e)
            {
                Log.Log.GravarLog("TiposFuncionarios","TipoFuncionarioDAO",e.StackTrace,Constantes.ATipoMetodo.SELECT);
                return null;
            }
        }
    }
}
