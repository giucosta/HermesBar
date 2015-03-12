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
                AO.GetCommand();
                AO.InsertParameter("Nome",atracoes.Nome);
                AO.InsertParameter("Estilo", atracoes.Estilo);
                AO.InsertParameter("Contato", atracoes.Contato.Id);
                AO.InsertParameter("Tempo_Show", atracoes.Tempo_Show);
                AO.InsertParameter("Ultimo_Valor_Cobrado", atracoes.Ultimo_Valor_Cobrado);

                return AO.ExecuteCommand();
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
                AO.GetCommand();
                AO.InsertParameter("Id_Atracoes",atracoes.Id);
                return AO.ExecuteCommand();
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
                AO.GetCommand();
                AO.InsertParameter(ConstantesDAO.WHERE, "Nome", ConstantesDAO.LIKE, atracoes.Nome);
                AO.InsertParameter(ConstantesDAO.AND, "Estilo", ConstantesDAO.LIKE, atracoes.Estilo);

                return AO.GetDataTable();
            }
            catch (Exception e)
            {
                Log.Log.GravarLog("Pesquisa","AtracoesDAO",e.StackTrace,Constantes.ATipoMetodo.SELECT);
                return null;
            }
        }
        public DataTable RetornaTodasAtracoes()
        {
            try
            {
                AccessObject<AtracoesModel> AO = new AccessObject<AtracoesModel>();
                AO.CreateSelectAll();
                AO.GetCommand();
                return AO.GetDataTable();
            }
            catch (Exception e)
            {
                Log.Log.GravarLog("RetornaTodasAtracoes", "AtracoesDAO", e.StackTrace, Constantes.ATipoMetodo.SELECT);
                throw;
            }
        }
        public DataTable RecuperaEstilos()
        {
            try
            {
                AccessObject<AtracoesModel> AO = new AccessObject<AtracoesModel>();
                AO.CreateSelectWithSimpleParameter("Estilo");
                AO.GetCommand();

                return AO.GetDataTable();
            }
            catch (Exception e)
            {
                Log.Log.GravarLog("RecuperaEstilos", "AtracoesDAO", e.StackTrace, Constantes.ATipoMetodo.SELECT);
                return null;
            }
        }
    }
}
