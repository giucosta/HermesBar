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
    public class EnderecoDAO
    {
        public EnderecoModel Salvar(EnderecoModel endereco)
        {
            try
            {
                AccessObject<EnderecoModel> AO = new AccessObject<EnderecoModel>();
                AO.CreateDataInsert();
                AO.GetCommand();
                AO.InsertParameter("Rua",endereco.Rua);
                AO.InsertParameter("Numero",endereco.Numero);
                AO.InsertParameter("Complemento",endereco.Complemento);
                AO.InsertParameter("Bairro",endereco.Bairro);
                AO.InsertParameter("Cep",endereco.Cep);
                AO.InsertParameter("Cidade",endereco.Cidade);
                AO.InsertParameter("Estado",endereco.Estado);
                AO.InsertParameter("Tipo", endereco.Tipo.Tipo);
                
                if(AO.ExecuteCommand())
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
                AO.GetCommand();
                return CarregaEndereco(AO.GetDataTable());
            }
            catch (Exception e)
            {
                Log.Log.GravarLog("RecuperaUltimoEndereco","EnderecoDAO",e.StackTrace,Constantes.ATipoMetodo.SELECT);
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
                AccessObject<EnderecoModel> AO = new AccessObject<EnderecoModel>();
                AO.CreateSelectAll();
                AO.GetCommand();
                AO.InsertParameter(ConstantesDAO.WHERE, "Id_Endereco", ConstantesDAO.EQUAL, id);
                
                return CarregaEndereco(AO.GetDataTable());
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
                AO.GetCommand();
                AO.InsertParameter("Id_Endereco", endereco.Id);
                return AO.ExecuteCommand();
            }
            catch (Exception e)
            {
                Log.Log.GravarLog("Excluir","EnderecoDAO",e.StackTrace,Constantes.ATipoMetodo.DELETE);
                return false;
            }
        }
        public bool Editar(EnderecoModel endereco)
        {
            try
            {
                AccessObject<EnderecoModel> AO = new AccessObject<EnderecoModel>();
                AO.CreateSpecificQuery("UPDATE Endereco SET Rua = @Rua, Numero = @Numero, Bairro = @Bairro, Complemento = @Complemento, Cep = @Cep, Cidade = @Cidade, Estado = @Estado");
                AO.GetCommand();
                AO.InsertParameter(ConstantesDAO.WHERE, "Id_Endereco", ConstantesDAO.EQUAL, endereco.Id);
                AO.InsertParameter("Rua",endereco.Rua);
                AO.InsertParameter("Numero", endereco.Numero);
                AO.InsertParameter("Bairro", endereco.Bairro);
                AO.InsertParameter("Complemento", endereco.Complemento);
                AO.InsertParameter("Cep", endereco.Cep);
                AO.InsertParameter("Cidade", endereco.Cidade);
                AO.InsertParameter("Estado", endereco.Estado);

                return AO.ExecuteCommand();
            }
            catch (Exception e)
            {
                Log.Log.GravarLog("Editar", "EnderecoDAO", e.Message, Constantes.ATipoMetodo.UPDATE);
                return false;
            }
        }
    }
}
