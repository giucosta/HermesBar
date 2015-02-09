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
    public class ConfigEstabelecimentoEstabelecimentoDAO
    {
        public bool Salvar(EstabelecimentoModel estabelecimento)
        {
            try
            {
                var sql = "INSERT INTO ConfigEstabelecimento_Estabelecimento VALUES(@idEstabelecimento, @idConfigEstabelecimento)";
                var comando = new SqlCommand(sql, Connection.GetConnection());
                comando.Parameters.AddWithValue("@idEstabelecimento", estabelecimento.Id);
                comando.Parameters.AddWithValue("@idConfigEstabelecimento", estabelecimento.ConfigEstabelecimento.Id);

                Connection.ExecutarComando(comando);
                return true;
            }
            catch (Exception e)
            {
                Log.Log.GravarLog("Salvar","ConfigEstabelecimentoEstabelecimento",e.StackTrace,Constantes.ATipoMetodo.INSERT);
                return false;
            }
        }
    }
}
