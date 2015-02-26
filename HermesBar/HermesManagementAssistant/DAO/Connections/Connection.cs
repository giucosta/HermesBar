﻿using System;
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

        public static SqlConnection GetConnection(){
            _connection = new SqlConnection(_connectionString);
            try
            {
                _connection.Open();
            }
            catch(SqlException)
            {
                Log.Log.GravarLog("GetConnection", "Connection", "Falha ao conectar na base de dados", "");
                _connection = null;
            }
            return _connection;
        }
        public static bool ExecutarComando(SqlCommand command)
        {
            try
            {
                command.ExecuteNonQuery();
                OutConnection();
                return true;
            }
            catch(SqlException e)
            {
                Log.Log.GravarLog("ExecutarComando","Connection",command.CommandText,"");
                OutConnection();
                return false;
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

