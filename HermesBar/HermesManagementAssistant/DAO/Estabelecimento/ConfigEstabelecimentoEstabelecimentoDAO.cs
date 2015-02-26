using DAO.Connections;
using MODEL.Estabelecimento;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO.Utils;
using UTILS;

namespace DAO.Estabelecimento
{
    public class ConfigEstabelecimentoEstabelecimentoDAO
    {
        public bool Salvar(EstabelecimentoModel estabelecimento)
        {
            try
            {
                AccessObject<EstabelecimentoModel> AO = new AccessObject<EstabelecimentoModel>();
                AO.CreateSpecificQuery("INSERT INTO ConfigEstabelecimento_Estabelecimento VALUES (@idEstabelecimento , @idConfigEstabelecimento)");
                var comando = new SqlCommand(AO.ReturnQuery(), Connection.GetConnection());
                comando.Parameters.AddWithValue("@idEstabelecimento", estabelecimento.Id);
                comando.Parameters.AddWithValue("@idConfigEstabelecimento", estabelecimento.ConfigEstabelecimento.Id);

                return Connection.ExecutarComando(comando);
            }
            catch (Exception e)
            {
                Log.Log.GravarLog("Salvar","ConfigEstabelecimentoEstabelecimento",e.StackTrace,Constantes.ATipoMetodo.INSERT);
                return false;
            }
        }
    }
}
