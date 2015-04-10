using DAO.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UTIL;

namespace DAO.Connections
{
    public class Connection
    {
        private static string _connectionString = Constantes.AConexaoBanco.SERVER + Constantes.AConexaoBanco.DATABASE + Constantes.AConexaoBanco.SEGURANCA;
        private static SqlConnection _connection = null;
        private static SqlTransaction _transaction = null;
        private static SqlParameter _parameter = null;
        public static SqlCommand _command = null;

        public static SqlConnection GetConnection(){
            if (_transaction == null)
            {
                if (_connection != null)
                    OutConnection();
                _connection = new SqlConnection(_connectionString);
                try
                {
                    _connection.Open();
                }
                catch (SqlException e)
                {
                    Log.Log.GravarLog("GetConnection", "Connection", "Falha ao conectar na base de dados", "");
                    _connection = null;
                    throw e;
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
                throw e;
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
            catch(SqlException e)
            {
                OutConnection();
                throw e;
            }
        }
        public static void OutConnection(){
            if (_connection != null)
                _connection.Close();
            ResetParameters();
        }
        public static SqlCommand GetCommand(string AO)
        {

            _parameter = null;
            _command = new SqlCommand(AO,GetConnection(),_transaction);
            return _command;
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
        private static void ResetParameters()
        {
            _command = null;
            _connection = null;
            _transaction = null;
            _parameter = null;
        }
    }
}

