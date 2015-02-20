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
using UTILS;

namespace DAO.Atracoes
{
    public class AtracoesDAO : IAtracoes
    {
        public bool Salvar(AtracoesModel atracoes)
        {
            try
            {
                AccessObject<AtracoesModel> AO = new AccessObject<AtracoesModel>();
                AO.CreateDataInsert();

                var comando = new SqlCommand(AO.ReturnQuery(), Connection.GetConnection());
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
                AccessObject<AtracoesModel> AO = new AccessObject<AtracoesModel>();
                AO.DeleteFromId();
                var comando = new SqlCommand(AO.ReturnQuery(),Connection.GetConnection());
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
                AccessObject<AtracoesModel> AO = new AccessObject<AtracoesModel>();
                AO.CreateSelectAll();
                AO.InsertParameter(ConstantesDAO.WHERE,"1=1");
                AO.InsertParameter(ConstantesDAO.AND, "Nome");
                AO.InsertParameter(ConstantesDAO.LIKE, "@Nome");
                AO.InsertParameter(ConstantesDAO.AND,"Estilo");
                AO.InsertParameter(ConstantesDAO.LIKE,"@Estilo");

                var comando = new SqlCommand(AO.ReturnQuery(),Connection.GetConnection());

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
                AccessObject<AtracoesModel> AO = new AccessObject<AtracoesModel>();
                AO.CreateSelectWithSimpleParameter("Estilo");
                var comando = new SqlCommand(AO.ReturnQuery(), Connection.GetConnection());

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
