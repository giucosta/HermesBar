using DAO.Connections;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTILS;

namespace DAO.Log
{
    public static class Log
    {
        public static void GravarLog(string Metodo, string Classe)
        {
            try
            {
                var sql = @"INSERT INTO Logs VALUES(@Metodo, @Classe, @Data, @Usuario)";
                var comando = new SqlCommand(sql, Connection.getConnection());
                comando.Parameters.Add(new SqlParameter("@Metodo", Metodo));
                comando.Parameters.Add(new SqlParameter("@Classe", Classe));
                comando.Parameters.Add(new SqlParameter("@Data", DateTime.Now));
                comando.Parameters.Add(new SqlParameter("@Usuario", Session.Usuario.Nome));

                Connection.ExecutarComando(comando);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
