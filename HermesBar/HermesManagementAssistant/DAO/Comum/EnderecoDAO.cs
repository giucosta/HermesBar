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
    public class EnderecoDAO
    {
        public EnderecoModel Salvar(EnderecoModel endereco)
        {
            var sql = @"INSERT INTO Endereco VALUES (
                            @Rua, 
                            @Numero, 
                            @Complemento, 
                            @Bairro, 
                            @Cep, 
                            @Cidade, 
                            @Estado, 
                            @IdTipoEndereco
                        )";
            var comando = new SqlCommand(sql, Connection.GetConnection());
            comando.Parameters.AddWithValue("@Rua",endereco.Rua);
            comando.Parameters.AddWithValue("@Numero",endereco.Numero);
            comando.Parameters.AddWithValue("@Complemento",endereco.Complemento);
            comando.Parameters.AddWithValue("@Bairro", endereco.Bairro);
            comando.Parameters.AddWithValue("@Cep",endereco.Cep);
            comando.Parameters.AddWithValue("@Cidade",endereco.Cidade);
            comando.Parameters.AddWithValue("@Estado", endereco.Estado);
            comando.Parameters.AddWithValue("@IdTipoEndereco", endereco.Tipo.Tipo);

            Connection.ExecutarComando(comando);
            return RecuperaUltimoEndereco();
        }

        public TipoEnderecoModel RetornaTipoEndereco(EnderecoModel endereco)
        {
            return new TipoEnderecoDAO().RetornaTipoEndereco(endereco);
        }
        public EnderecoModel RecuperaUltimoEndereco()
        {
            try
            {
                var sql = "SELECT TOP 1 * FROM Endereco ORDER BY Id_Endereco DESC";
                var comando = new SqlCommand(sql, Connection.GetConnection());
                return CarregaEndereco(Connection.getDataTable(comando));
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
                var sql = "SELECT * FROM Endereco WHERE Id_Endereco = @Id";
                var comando = new SqlCommand(sql, Connection.GetConnection());
                comando.Parameters.AddWithValue("Id",id);

                return CarregaEndereco(Connection.getDataTable(comando));
            }
            catch (Exception e)
            {
                Log.Log.GravarLog("RecuperaEnderecoPeloId","EnderecoDAO",e.StackTrace,Constantes.ATipoMetodo.SELECT);
                return null;
            }
        }
    }
}
