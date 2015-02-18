using DAO.Connections;
using MODEL.Estabelecimento;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO;
using DAO.Utils;

namespace DAO.Estabelecimento
{
    public class ConfigEstabelecimentoEstabelecimentoDAO
    {
        public bool Salvar(EstabelecimentoModel estabelecimento)
        {
            try
            {
                AccessObject<EstabelecimentoModel> AO = new AccessObject<EstabelecimentoModel>();
                AO.InsertParameter(ConstantesDAO.INSERT,ConstantesDAO.INTO);
                AO.InsertParameter("ConfigEstabelecimento_Estabelecimento",ConstantesDAO.VALUES);
                AO.InsertParameter("(@idEstabelecimento", " @idConfigEstabelecimento)");
                var comando = new SqlCommand(AO.ReturnQuery(), Connection.GetConnection());
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
