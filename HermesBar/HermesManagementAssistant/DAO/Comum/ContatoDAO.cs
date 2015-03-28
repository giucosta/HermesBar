using DAO.Abstract;
using DAO.Connections;
using MODEL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTIL;
using DAO.Utils;

namespace DAO.Comum
{
    public class ContatoDAO
    {
        public ContatoModel Salvar(ContatoModel contato)
        {
            try
            {
                AccessObject<ContatoModel> AO = new AccessObject<ContatoModel>();
                AO.CreateDataInsert();
                AO.GetCommand();
                AO.InsertParameter("Nome",contato.Nome);
                AO.InsertParameter("Telefone",contato.Telefone);
                AO.InsertParameter("Celular",contato.Celular);
                AO.InsertParameter("Email",contato.Email);
                AO.InsertParameter("Site",contato.Site);
                
                if (AO.ExecuteCommand())
                    return Pesquisa(contato);
                return null;
            }
            catch (Exception e)
            {
                Log.Log.GravarLog("Salvar","ContatoDAO",e.StackTrace,Constantes.ATipoMetodo.INSERT);
                return null;
            }
        }
        public bool Excluir(ContatoModel contato)
        {
            try
            {
                AccessObject<ContatoModel> AO = new AccessObject<ContatoModel>();
                AO.DeleteFromId();
                AO.GetCommand();
                AO.InsertParameter("Id_Contato", contato.Id);

                return AO.ExecuteCommand();
            }
            catch (Exception e)
            {
                Log.Log.GravarLog("Excluir", "ContatoDAO", e.StackTrace, Constantes.ATipoMetodo.DELETE);
                return false;
            }
        }
        public ContatoModel Pesquisa(ContatoModel contato)
        {
            try
            {
                AccessObject<ContatoModel> AO = new AccessObject<ContatoModel>();
                AO.CreateSelectAll();
                AO.GetCommand();
                AO.InsertParameter(ConstantesDAO.WHERE, "Nome", ConstantesDAO.LIKE, contato.Nome);
                AO.InsertParameter(ConstantesDAO.AND, "Email", ConstantesDAO.LIKE, contato.Email);

                return CarregaContato(AO.GetDataTable());
            }
            catch (Exception e)
            {
                Log.Log.GravarLog("Pesquisa","ContatoDAO",e.StackTrace,Constantes.ATipoMetodo.SELECT);
                return null;
            }
        }
        public ContatoModel PesquisaContatoId(int id)
        {
            try
            {
                AccessObject<ContatoModel> AO = new AccessObject<ContatoModel>();
                AO.CreateSelectAll();
                AO.GetCommand();
                AO.InsertParameter(ConstantesDAO.WHERE, "Id_Contato", ConstantesDAO.EQUAL, id);
                return CarregaContato(AO.GetDataTable());
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<ContatoModel> CarregaListModel(DataTable data)
        {
            var contatoList = new List<ContatoModel>();
            for (int i = 0; i < data.Rows.Count; i++)
            {
                var contato = new ContatoModel();
                contato.Id = (int)data.Rows[i]["Id_Contato"];
                contato.Nome = data.Rows[i]["Nome"].ToString();
                contato.Telefone = data.Rows[i]["Telefone"].ToString();
                contato.Celular = data.Rows[i]["Celular"].ToString();
                contato.Email = data.Rows[i]["Email"].ToString();
                contato.Site = data.Rows[i]["Site"].ToString();

                contatoList.Add(contato);
            }
            return contatoList;
        }
        private ContatoModel CarregaContato(DataTable data)
        {
            if (data != null)
            {
                var contato = new ContatoModel();
                contato.Id = (int)data.Rows[0]["Id_Contato"];
                contato.Nome = data.Rows[0]["Nome"].ToString();
                contato.Telefone = data.Rows[0]["Telefone"].ToString();
                contato.Celular = data.Rows[0]["Celular"].ToString();
                contato.Email = data.Rows[0]["Email"].ToString();
                contato.Site = data.Rows[0]["Site"].ToString();

                return contato;
            }
            return null;
        }
        public ContatoModel RecuperaContatoPeloId(int id)
        {
            try
            {
                AccessObject<ContatoModel> AO = new AccessObject<ContatoModel>();
                AO.CreateSelectAll();
                AO.GetCommand();
                AO.InsertParameter(ConstantesDAO.WHERE, "Id_Contato", ConstantesDAO.EQUAL, id);

                return CarregaContato(AO.GetDataTable());
            }
            catch (Exception e)
            {
                Log.Log.GravarLog("RecuperaContatoPeloId","ContatoDAO",e.StackTrace,Constantes.ATipoMetodo.SELECT);
                return null;
            }
        }
        public int RecuperaProximoId()
        {
            AccessObject<ContatoModel> AO = new AccessObject<ContatoModel>();
            AO.CreateSpecificQuery("SELECT MAX(Id_Contato) + 1 as Proximo FROM Contato");

            Connection.GetCommand(AO.ReturnQuery());
            return (int)Connection.getDataTable().Rows[0]["Proximo"];
        }
        public bool Editar(ContatoModel contato)
        {
            try
            {
                AccessObject<ContatoModel> AO = new AccessObject<ContatoModel>();
                AO.CreateSpecificQuery("UPDATE CONTATO Set Nome = @Nome, Telefone = @Telefone, Celular = @Celular, Email = @Email, Site = @Site");
                AO.GetCommand();
                AO.InsertParameter(ConstantesDAO.WHERE, "Id_Contato", ConstantesDAO.EQUAL, contato.Id);
                AO.InsertParameter("Nome", contato.Nome);
                AO.InsertParameter("Telefone", contato.Telefone);
                AO.InsertParameter("Celular", contato.Celular);
                AO.InsertParameter("Email", contato.Email);
                AO.InsertParameter("Site", contato.Site);

                return AO.ExecuteCommand();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
