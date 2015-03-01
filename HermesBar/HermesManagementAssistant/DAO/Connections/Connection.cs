using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTILS;

namespace DAO.Connections
{
    public class Connection
    {
        private static string _connectionString = Constantes.AConexaoBanco.SERVER + Constantes.AConexaoBanco.DATABASE + Constantes.AConexaoBanco.SEGURANCA;
        private static SqlConnection _connection = null;
        private static SqlTransaction _transaction = null;
        private static SqlParameter _parameter = null;
        private static SqlCommand _command = null;

        public static SqlConnection GetConnection(){
            if (_connection == null)
            {
                _connection = new SqlConnection(_connectionString);
                try
                {
                    _connection.Open();
                }
                catch (SqlException)
                {
                    Log.Log.GravarLog("GetConnection", "Connection", "Falha ao conectar na base de dados", "");
                    _connection = null;
                }
            }
            return _connection;
        }
        public static bool ExecutarComando()
        {
            try
            {
                _command.ExecuteNonQuery();
                return true;
            }
            catch(SqlException e)
            {
                Log.Log.GravarLog("ExecutarComando","Connection",_command.CommandText,"");
                OutConnection();
                return false;
            }
        }
        public static DataTable getDataTable()
        {
            try
            {
                var dataTable = new DataTable();
                dataTable.Load(_command.ExecuteReader());
                _command = null;

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
            _connection = null;
            _transaction = null;
        }
        public static SqlCommand GetCommand(string AO)
        {
            _parameter = null;
            return new SqlCommand(AO, GetConnection(), _transaction);
        }
        public static void GetTransaction()
        {
            _transaction = GetConnection().BeginTransaction();
        }
        public static void Commit()
        {
            _transaction.Commit();
            OutConnection();
        }
        public static void Rollback()
        {
            _transaction.Rollback();
            OutConnection();
        }
        public static void AddParameter(string attribute, Object attributeModel)
        {
            _command.Parameters.AddWithValue(attribute, attributeModel);
        }
    }
}

