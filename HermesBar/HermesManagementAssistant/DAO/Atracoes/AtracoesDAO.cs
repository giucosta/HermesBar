﻿using DAO.Abstract;
using DAO.Connections;
using MODEL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTILS;

namespace DAO.Atracoes
{
    public class AtracoesDAO : IAtracoes
    {
        public bool Salvar(AtracoesModel atracoes)
        {
            try
            {
                var sql = @"INSERT INTO Atracoes VALUES (@Nome, @Estilo, @IdContato, @TempoShow, @UltimoValor)";
                var comando = new SqlCommand(sql, Connection.GetConnection());
                comando.Parameters.AddWithValue("@Nome",atracoes.Nome);
                comando.Parameters.AddWithValue("@Estilo", atracoes.EstiloPredominante);
                comando.Parameters.AddWithValue("@IdContato", atracoes.Contato.Id);
                comando.Parameters.AddWithValue("@TempoShow", atracoes.TempoApresentacao);
                comando.Parameters.AddWithValue("@UltimoValor", atracoes.UltimoValorCobrado);

                Connection.ExecutarComando(comando);
                return true;
            }
            catch (Exception e)
            {
                Log.Log.GravarLog("Salvar", "AtracoesDAO", e.StackTrace, Constantes.ATipoMetodo.INSERT);
                return false;
            }
        }
        public bool Excluir(AtracoesModel atracoes)
        {
            try
            {
                var sql = @"DELETE FROM Atracoes WHERE Id_Atracoes = @Id";
                var comando = new SqlCommand(sql,Connection.GetConnection());
                comando.Parameters.AddWithValue("@Id",atracoes.Id);
                Connection.ExecutarComando(comando);
                return true;
            }
            catch (Exception e)
            {
                Log.Log.GravarLog("Excluir", "ExcluirDAO", e.StackTrace, Constantes.ATipoMetodo.DELETE);
                return false;
            }
        }
        public DataTable Pesquisa(AtracoesModel atracoes)
        {
            try
            {
                var sql = @"SELECT * FROM Atracoes 
                            WHERE 1=1 
                            AND Nome LIKE @Nome
                            AND Estilo LIKE @Estilo";
                var comando = new SqlCommand(sql,Connection.GetConnection());

                comando.Parameters.AddWithValue("@Nome", "%" + atracoes.Nome + "%");
                comando.Parameters.AddWithValue("@Estilo", "%" + atracoes.EstiloPredominante + "%");

                return Connection.getDataTable(comando);
            }
            catch (Exception e)
            {
                Log.Log.GravarLog("Pesquisa","AtracoesDAO",e.StackTrace,Constantes.ATipoMetodo.SELECT);
                return null;
            }
        }
        public List<String> RecuperaEstilos()
        {
            try
            {
                var sql = @"SELECT Estilo FROM Atracoes";
                var comando = new SqlCommand(sql, Connection.GetConnection());

                var dataTable = Connection.getDataTable(comando);
                var lista = new List<String>();
                for (int i = 0; i < dataTable.Rows.Count; i++)
                    lista.Add(dataTable.Rows[i]["Estilo"].ToString());

                return lista;
            }
            catch (Exception e)
            {
                Log.Log.GravarLog("RecuperaEstilos", "AtracoesDAO", e.StackTrace, Constantes.ATipoMetodo.SELECT);
                return null;
            }
        }
    }
}
