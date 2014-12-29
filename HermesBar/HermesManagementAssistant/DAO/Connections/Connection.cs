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
        public static void ExecutarComando(SqlCommand command)
        {
            try
            {
                command.ExecuteNonQuery();
            }
            catch(SqlException)
            {
                outConnection();
            }
        }
        public static DataTable getDataTable(SqlCommand command)
        {
            try
            {
                var dataTable = new DataTable();
                dataTable.Load(command.ExecuteReader());

                outConnection();
                return dataTable;
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

