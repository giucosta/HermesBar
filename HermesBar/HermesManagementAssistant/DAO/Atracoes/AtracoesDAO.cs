using DAO.Abstract;
using DAO.Connections;
using DAO.Utils;
using MODEL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DAO;

namespace DAO.Atracoes
{
    public class AtracoesDAO : IAtracoes
    {
        public bool Salvar(AtracoesModel atracoes)
        {
            try
            {
                var stb = AccessObject<AtracoesModel>.CreateDataInsert();
                var comando = new SqlCommand(stb.ToString(), Connection.GetConnection());
                comando.Parameters.AddWithValue("@Nome",atracoes.Nome);
                comando.Parameters.AddWithValue("@Estilo", atracoes.Estilo);
                comando.Parameters.AddWithValue("@Contato", atracoes.Contato.Id);
                comando.Parameters.AddWithValue("@Tempo_Show", atracoes.Tempo_Show);
                comando.Parameters.AddWithValue("@Ultimo_Valor_Cobrado", atracoes.Ultimo_Valor_Cobrado);

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
                var comando = new SqlCommand(AccessObject<AtracoesModel>.DeleteFromId(),Connection.GetConnection());
                comando.Parameters.AddWithValue("@Id_Atracoes",atracoes.Id);
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
                var sql = AccessObject<AtracoesModel>.CreateSelectAll();
                sql = AccessObject<AtracoesModel>.InsertParameter(sql,ConstantesDAO.WHERE,"1=1");
                sql = AccessObject<AtracoesModel>.InsertParameter(sql, ConstantesDAO.AND, "Nome");
                sql = AccessObject<AtracoesModel>.InsertParameter(sql, ConstantesDAO.LIKE, "@Nome");
                sql = AccessObject<AtracoesModel>.InsertParameter(sql, ConstantesDAO.AND,"Estilo");
                sql = AccessObject<AtracoesModel>.InsertParameter(sql, ConstantesDAO.LIKE,"@Estilo");

                var comando = new SqlCommand(sql,Connection.GetConnection());

                comando.Parameters.AddWithValue("@Nome", "%" + atracoes.Nome + "%");
                comando.Parameters.AddWithValue("@Estilo", "%" + atracoes.Estilo + "%");

                return Connection.getDataTable(comando);
            }
            catch (Exception e)
            {
                Log.Log.GravarLog("Pesquisa","AtracoesDAO",e.StackTrace,Constantes.ATipoMetodo.SELECT);
                return null;
            }
        }
        public DataTable RecuperaEstilos()
        {
            try
            {
                var sql = AccessObject<AtracoesModel>.CreateSelectWithSimpleParameter("Estilo");
                var comando = new SqlCommand(sql, Connection.GetConnection());

                return Connection.getDataTable(comando);
            }
            catch (Exception e)
            {
                Log.Log.GravarLog("RecuperaEstilos", "AtracoesDAO", e.StackTrace, Constantes.ATipoMetodo.SELECT);
                return null;
            }
        }
    }
}
