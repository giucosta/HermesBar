using DAO.Connections;
using MODEL.Estabelecimento;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO.Utils;
using UTIL;

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
                AO.GetCommand();
                AO.InsertParameter("IdEstabelecimento", estabelecimento.Id);
                AO.InsertParameter("idConfigEstabelecimento",estabelecimento.ConfigEstabelecimento.Id);

                return AO.ExecuteCommand();
            }
            catch (Exception e)
            {
                Log.Log.GravarLog("Salvar","ConfigEstabelecimentoEstabelecimento",e.StackTrace,Constantes.ATipoMetodo.INSERT);
                return false;
            }
        }
    }
}
