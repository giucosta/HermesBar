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
using UTIL;

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
                AO.InsertParameter("Estilo", atracoes.Estilo.Id);
                AO.InsertParameter("Contato", atracoes.Contato.Id);
                AO.InsertParameter("Tempo_Show", atracoes.Tempo_Show);
                AO.InsertParameter("Ultimo_Valor_Cobrado", atracoes.Ultimo_Valor_Cobrado);

                return AO.ExecuteCommand();
            }
            catch (Exception e)
            {
                Log.Log.GravarLog("Salvar", "AtracoesDAO", e.Message, Constantes.ATipoMetodo.INSERT);
                throw e;
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
                if(atracoes.Estilo.Id != 0)
                    AO.InsertParameter(ConstantesDAO.AND, "Id_EstiloAtracoes", ConstantesDAO.LIKE, atracoes.Estilo.Id);

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
                AO.CreateSelectWithSimpleParameter(ConstantesDAO.DISTINCT + " Estilo");
                AO.GetCommand();

                return AO.GetDataTable();
            }
            catch (Exception e)
            {
                Log.Log.GravarLog("RecuperaEstilos", "AtracoesDAO", e.StackTrace, Constantes.ATipoMetodo.SELECT);
                return null;
            }
        }
        public DataTable RecuperaIdContato(AtracoesModel atracao)
        {
            try
            {
                AccessObject<AtracoesModel> AO = new AccessObject<AtracoesModel>();
                AO.CreateSelectWithSimpleParameter("Id_Contato");
                AO.GetCommand();
                AO.InsertParameter(ConstantesDAO.WHERE, "Id_Atracoes", ConstantesDAO.EQUAL, atracao.Id);
                return AO.GetDataTable();
            }
            catch (Exception e)
            {
                Log.Log.GravarLog("RecuperaIdContato", "AtracoesDAO", e.Message, Constantes.ATipoMetodo.SELECT);
                return null;
            }
        }
        public DataTable RecuperaIdEstilo(AtracoesModel atracao)
        {
            try
            {
                AccessObject<AtracoesModel> AO = new AccessObject<AtracoesModel>();
                AO.CreateSelectWithSimpleParameter("Id_EstiloAtracoes");
                AO.GetCommand();
                AO.InsertParameter(ConstantesDAO.WHERE, "Id_Atracoes", ConstantesDAO.EQUAL, atracao.Id);
                return AO.GetDataTable();
            }
            catch (Exception e)
            {
                Log.Log.GravarLog("RecuperaIdEstilo", "AtracoesDAO", e.Message, Constantes.ATipoMetodo.SELECT);
                return null;
            }
        }
        public bool Editar(AtracoesModel atracao)
        {
            try
            {
                AccessObject<AtracoesModel> AO = new AccessObject<AtracoesModel>();
                AO.CreateSpecificQuery("UPDATE Atracoes SET Nome = @Nome, Id_EstiloAtracoes = @Estilo, Ultimo_Valor_Cobrado = @Ultimo_Valor_Cobrado, Tempo_Show = @Tempo_Show");
                AO.GetCommand();
                AO.InsertParameter(ConstantesDAO.WHERE, "Id_Atracoes", ConstantesDAO.EQUAL, atracao.Id);
                AO.InsertParameter("Nome",atracao.Nome);
                AO.InsertParameter("Estilo", atracao.Estilo.Id);
                AO.InsertParameter("Ultimo_Valor_Cobrado", atracao.Ultimo_Valor_Cobrado);
                AO.InsertParameter("Tempo_Show", atracao.Tempo_Show);
                return AO.ExecuteCommand();
            }
            catch (Exception e)
            {
                Log.Log.GravarLog("Editar", "AtracoesDAO", e.StackTrace, Constantes.ATipoMetodo.UPDATE);
                return false;
            }
        }
    }
}
