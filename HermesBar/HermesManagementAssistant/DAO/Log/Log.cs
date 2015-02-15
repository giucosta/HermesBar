using DAO.Connections;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO;

namespace DAO.Log
{
    public static class Log
    {
        public static void GravarLog(string Metodo, string Classe, string Erro, string Tipo)
        {
            try
            {
                var sql = @"INSERT INTO Logs VALUES(@Metodo, @Classe, @Data, @Usuario, @Erro, @Tipo)";
                var comando = new SqlCommand(sql, Connection.GetConnection());
                comando.Parameters.AddWithValue("@Metodo", Metodo);
                comando.Parameters.AddWithValue("@Classe", Classe);
                comando.Parameters.AddWithValue("@Data",DateTime.Now);
                comando.Parameters.AddWithValue("@Usuario",Session.Usuario.Nome);
                comando.Parameters.AddWithValue("@Erro",Erro);
                comando.Parameters.AddWithValue("@Tipo",Tipo);
                
                Connection.ExecutarComando(comando);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
