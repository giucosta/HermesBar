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
        private static string _connectionString = @"server = GIULIANOCOSTA\SQLEXPRESS; Database = HermesBar; integrated security = true;";
        private static SqlConnection _connection = null;   

        public static SqlConnection GetConnection(){
            _connection = new SqlConnection(_connectionString);
            try
            {
                _connection.Open();
            }
            catch(SqlException)
            {
                _connection = null;
            }
            return _connection;
        }
        public static void ExecutarComando(SqlCommand command)
        {
            try
            {
                command.ExecuteNonQuery();
                OutConnection();
            }
            catch(SqlException)
            {
                OutConnection();
            }
        }
        public static DataTable getDataTable(SqlCommand command)
        {
            try
            {
                var dataTable = new DataTable();
                dataTable.Load(command.ExecuteReader());

                OutConnection();
                return dataTable;
            }
            catch(SqlException)
            {
                OutConnection();
                return null;
            }
        }
        public static void OutConnection(){
            if (_connection != null)
                _connection.Close();
        }
    }
}

