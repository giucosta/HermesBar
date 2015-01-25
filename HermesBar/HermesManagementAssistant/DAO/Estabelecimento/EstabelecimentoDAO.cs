using DAO.Connections;
using MODEL.Estabelecimento;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTILS;

namespace DAO.Estabelecimento
{
    public class EstabelecimentoDAO
    {
        public bool Salvar(EstabelecimentoModel estabelecimento)
        {
            try
            {
                var sql = @"INSERT INTO Estabelecimento VALUES(@RazaoSocial,@NomeFantasia,@Cnpj,@InscricaoEstadual,@Id_Endereco,@Id_Contato)";
                var comando = new SqlCommand(sql,Connection.GetConnection());
                comando.Parameters.AddWithValue("@RazaoSocial",estabelecimento.RazaoSocial);
                comando.Parameters.AddWithValue("@NomeFantasia",estabelecimento.NomeFantasia);
                comando.Parameters.AddWithValue("@Cnpj",estabelecimento.Cnpj);
                comando.Parameters.AddWithValue("@InscricaoEstadual",estabelecimento.InscEstadual);
                comando.Parameters.AddWithValue("@Id_Endereco",estabelecimento.Endereco.Id);
                comando.Parameters.AddWithValue("@Id_Contato",estabelecimento.Contato.Id);

                Connection.ExecutarComando(comando);
                return true;
            }
            catch (Exception e)
            {
                Log.Log.GravarLog("Salvar","EstabelecimentoDAO",e.StackTrace,Constantes.ATipoMetodo.INSERT);
                return false;
            }
        }
    }
}
