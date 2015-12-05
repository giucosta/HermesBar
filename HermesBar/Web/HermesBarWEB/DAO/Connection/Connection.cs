using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace DAO.Connection
{
    public class Connection
    {
        private string _connectionString = @"Data Source=GIULIANOCOSTA\SQLEXPRESS;Database = HERMESBARWEB2;Integrated Security=SSPI;";
        SqlConnection conn = null;
        SqlDataAdapter adapter = null;

        public void OpenConnection()
        {
            try
            {
                CloseConnection();

                conn = new SqlConnection(_connectionString);
                conn.Open();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void CloseConnection()
        {
            if (conn != null)
            {
                conn.Close();
                conn = null;
            }
            if (adapter != null)
                adapter = null;
        }
        public void CreateDataAdapter(string procedureName)
        {
            try
            {
                adapter = new SqlDataAdapter(procedureName, conn);
                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void InserParameter(string parameter, SqlDbType type, object value)
        {
            if (value != null)
                adapter.SelectCommand.Parameters.Add("@" + parameter, type).Value = value;
        }
        public DataTable GetResult()
        {
            DataTable data = new DataTable();
            adapter.Fill(data);
            CloseConnection();

            return data;
        }
        public DataSet GetResult(DataSet data)
        {
            adapter.Fill(data);
            CloseConnection();

            return data;
        }
        public DataSet GetResultAsDataSet()
        {
            DataSet data = new DataSet();
            adapter.Fill(data);
            CloseConnection();

            return data;
        }
        public string GetConnectionString()
        {
            return _connectionString;
        }
    }
}
