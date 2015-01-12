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

namespace DAO.Comum
{
    public class ContatoDAO
    {
        public ContatoModel Salvar(ContatoModel contato)
        {
            try
            {
                var sql = @"INSERT INTO Contato VALUES(@Nome,@Telefone,@Celular,@Email,@Site)";
                var comando = new SqlCommand(sql, Connection.GetConnection());
                comando.Parameters.AddWithValue("@Nome",contato.Nome);
                comando.Parameters.AddWithValue("@Telefone",contato.Telefone);
                comando.Parameters.AddWithValue("@Celular", contato.Celular);
                comando.Parameters.AddWithValue("Email",contato.Email);
                comando.Parameters.AddWithValue("@Site",contato.Site);

                Connection.ExecutarComando(comando);
                return Pesquisa(contato);
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
                var sql = @"DELETE Contato WHERE Id_Contato = @IdContato";
                var comando = new SqlCommand(sql, Connection.GetConnection());
                comando.Parameters.AddWithValue("@IdContato", contato.Id);

                Connection.ExecutarComando(comando);
                return true;
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
                var sql = @"SELECT * FROM Contato WHERE Nome LIKE @Nome AND Email LIKE @Email";
                var comando = new SqlCommand(sql,Connection.GetConnection());
                comando.Parameters.AddWithValue("@Nome", "%" + contato.Nome + "%");
                comando.Parameters.AddWithValue("@Email", "%" + contato.Email + "%");

                return CarregaModel(Connection.getDataTable(comando));
            }
            catch (Exception e)
            {
                Log.Log.GravarLog("Pesquisa","ContatoDAO",e.StackTrace,Constantes.ATipoMetodo.SELECT);
                return null;
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

        public ContatoModel CarregaModel(DataTable data)
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
    }
}
