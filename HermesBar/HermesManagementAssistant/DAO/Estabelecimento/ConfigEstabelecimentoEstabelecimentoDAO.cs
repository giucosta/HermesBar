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
                Connection.GetCommand(AO.ReturnQuery());
                Connection.AddParameter("@idEstabelecimento", estabelecimento.Id);
                Connection.AddParameter("@idConfigEstabelecimento", estabelecimento.ConfigEstabelecimento.Id);

                return Connection.ExecutarComando();
            }
            catch (Exception e)
            {
                Log.Log.GravarLog("Salvar","ConfigEstabelecimentoEstabelecimento",e.StackTrace,Constantes.ATipoMetodo.INSERT);
                return false;
            }
        }
    }
}
