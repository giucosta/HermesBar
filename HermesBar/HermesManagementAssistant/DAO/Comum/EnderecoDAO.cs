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
    public class EnderecoDAO
    {
        public EnderecoModel Salvar(EnderecoModel endereco)
        {
            try
            {
                var AO = new AccessObject<EnderecoModel>();
                AO.CreateDataInsert();

                Connection.GetCommand(AO.ReturnQuery());
                Connection.AddParameter("@Rua", endereco.Rua);
                Connection.AddParameter("@Numero", endereco.Numero);
                Connection.AddParameter("@Complemento", endereco.Complemento);
                Connection.AddParameter("@Bairro", endereco.Bairro);
                Connection.AddParameter("@Cep", endereco.Cep);
                Connection.AddParameter("@Cidade", endereco.Cidade);
                Connection.AddParameter("@Estado", endereco.Estado);
                Connection.AddParameter("@Tipo", endereco.Tipo.Tipo);
                
                if(Connection.ExecutarComando())
                    return RecuperaUltimoEndereco();
                return null;
            }
            catch (Exception e)
            {
                Log.Log.GravarLog("Salvar","EnderecoDAO",e.StackTrace.ToString(),Constantes.ATipoMetodo.INSERT);
                return null;
            }
        }
        public TipoEnderecoModel RetornaTipoEndereco(EnderecoModel endereco)
        {
            return new TipoEnderecoDAO().RetornaTipoEndereco(endereco);
        }
        public EnderecoModel RecuperaUltimoEndereco()
        {
            try
            {
                AccessObject<EnderecoModel> AO = new AccessObject<EnderecoModel>();
                AO.CreateSpecificQuery(@"SELECT TOP 1 * FROM Endereco ORDER BY Id_Endereco DESC");
                Connection.GetCommand(AO.ReturnQuery());
                return CarregaEndereco(Connection.getDataTable());
            }
            catch (Exception)
            {   
                throw;
            }
        }
        private EnderecoModel CarregaEndereco(DataTable endereco)
        {
            if (endereco != null)
            {
                var end = new EnderecoModel();
                end.Id = (int)endereco.Rows[0]["Id_Endereco"];
                end.Rua = endereco.Rows[0]["Rua"].ToString();
                end.Numero = endereco.Rows[0]["Numero"].ToString();
                end.Complemento = endereco.Rows[0]["Complemento"].ToString();
                end.Bairro = endereco.Rows[0]["Bairro"].ToString();
                end.Cep = endereco.Rows[0]["Cep"].ToString();
                end.Cidade = endereco.Rows[0]["Cidade"].ToString();
                end.Estado = endereco.Rows[0]["Estado"].ToString();
                end.Tipo = new TipoEnderecoModel() { Tipo = endereco.Rows[0]["TipoEndereco"].ToString() };

                return end;
            }
            return null;
        }
        public EnderecoModel RecuperaEnderecoPeloId(int id)
        {
            try
            {
                var AO = new AccessObject<EnderecoModel>();
                AO.CreateSelectAll();
                AO.InsertParameter(ConstantesDAO.WHERE, "Id_Endereco", ConstantesDAO.EQUAL, "@Id");
                Connection.GetCommand(AO.ReturnQuery());
                Connection.AddParameter("@Id", id);

                return CarregaEndereco(Connection.getDataTable());
            }
            catch (Exception e)
            {
                Log.Log.GravarLog("RecuperaEnderecoPeloId","EnderecoDAO",e.StackTrace,Constantes.ATipoMetodo.SELECT);
                return null;
            }
        }
        public bool Excluir(EnderecoModel endereco)
        {
            try
            {
                var AO = new AccessObject<EnderecoModel>();
                AO.DeleteFromId();
                Connection.GetCommand(AO.ReturnQuery());
                Connection.AddParameter("@Id_Endereco", endereco.Id);
                return Connection.ExecutarComando();
            }
            catch (Exception e)
            {
                Log.Log.GravarLog("Excluir","EnderecoDAO",e.StackTrace,Constantes.ATipoMetodo.DELETE);
                return false;
            }
        }
    }
}
