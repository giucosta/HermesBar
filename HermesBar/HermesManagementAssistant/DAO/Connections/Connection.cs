using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Connections
{
    public class Connection
    {
        private static string conectionString = @"server = GIULIANOCOSTA\SQLEXPRESS; Database = HermesBar; integrated security = true;";
        private static SqlConnection connection = null;   

        public static SqlConnection getConnection(){
            connection = new SqlConnection(conectionString);
            try
            {
                connection.Open();
            }
            catch(SqlException)
            {
                connection = null;
            }
            return connection;
        }
        public static void ExecutarComando(string strQuery)
        {
            try
            {
                var cmd = new SqlCommand(strQuery, getConnection());
                cmd.ExecuteNonQuery();
            }
            catch(SqlException)
            {
                outConnection();
            }
        }
        public static SqlDataReader RetornarDataReader(string strQuery)
        {
            try
            {
                var cmd = new SqlCommand(strQuery, getConnection());
                return cmd.ExecuteReader();
            }
            catch(SqlException)
            {
                outConnection();
                return null;
            }
        }
        public static void outConnection(){
            if (connection != null)
                connection.Close();
        }
    }
}

