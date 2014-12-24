using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Connections
{
    class Connection
    {
        private static string connString = @"server = GIULIANOCOSTA\SQLEXPRESS; Database = HermesBar; integrated security = true;";

        private static SqlConnection conn = null;   

        public static SqlConnection obterConexao(){
            conn = new SqlConnection(connString);
            try{
                conn.Open();
            }
            catch(SqlException sqle)
            {
                conn = null;
            }
            return conn;
        }

        public static void fecharConexao(){
          if(conn != null)
            conn.Close();
        }

    }


}

