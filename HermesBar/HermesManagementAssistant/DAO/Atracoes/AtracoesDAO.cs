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

                Connection.GetCommand(AO.ReturnQuery());
                Connection.AddParameter("@Nome", atracoes.Nome);
                Connection.AddParameter("@Estilo", atracoes.Estilo);
                Connection.AddParameter("@Contato", atracoes.Contato.Id);
                Connection.AddParameter("@Tempo_Show", atracoes.Tempo_Show);
                Connection.AddParameter("@Ultimo_Valor_Cobrado", atracoes.Ultimo_Valor_Cobrado);
                
                return Connection.ExecutarComando();
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
                Connection.GetCommand(AO.ReturnQuery());
                Connection.AddParameter("@Id_Atracoes", atracoes.Id);
                return Connection.ExecutarComando();
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
                AO.InsertParameter(ConstantesDAO.WHERE, "Nome", ConstantesDAO.LIKE, "@Nome");
                AO.InsertParameter(ConstantesDAO.AND, "Estilo", ConstantesDAO.LIKE, "@Estilo");

                Connection.GetCommand(AO.ReturnQuery());
                Connection.AddParameter("@Nome", "%" + atracoes.Nome + "%");
                Connection.AddParameter("@Estilo", "%" + atracoes.Estilo + "%");

                return Connection.getDataTable();
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
                Connection.GetCommand(AO.ReturnQuery());
                
                return Connection.getDataTable();
            }
            catch (Exception e)
            {
                Log.Log.GravarLog("RecuperaEstilos", "AtracoesDAO", e.StackTrace, Constantes.ATipoMetodo.SELECT);
                return null;
            }
        }
    }
}
