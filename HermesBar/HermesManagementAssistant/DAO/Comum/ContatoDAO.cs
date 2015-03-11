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
using UTILS;
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
                Connection.GetCommand(AO.ReturnQuery());
                Connection.AddParameter("@Nome", contato.Nome);
                Connection.AddParameter("@Telefone", contato.Telefone);
                Connection.AddParameter("@Celular", contato.Celular);
                Connection.AddParameter("@Email", contato.Email);
                Connection.AddParameter("@Site", contato.Site);

                if (Connection.ExecutarComando())
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
                Connection.GetCommand(AO.ReturnQuery());
                Connection.AddParameter("@Id_Contato", contato.Id);

                return Connection.ExecutarComando();
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
                AO.InsertParameter(ConstantesDAO.WHERE, "Nome", ConstantesDAO.LIKE, "@Nome");
                AO.InsertParameter(ConstantesDAO.AND, "Email", ConstantesDAO.LIKE, "@Email");

                Connection.GetCommand(AO.ReturnQuery());
                Connection.AddParameter("@Nome", "%" + contato.Nome + "%");
                Connection.AddParameter("@Email", "%" + contato.Email + "%");

                return CarregaContato(Connection.getDataTable());
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
                AO.InsertParameter(ConstantesDAO.WHERE, "Id_Contato", ConstantesDAO.EQUAL, "@Id");
                Connection.GetCommand(AO.ReturnQuery());
                Connection.AddParameter("@Id", id);

                return CarregaContato(Connection.getDataTable());
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
    }
}
