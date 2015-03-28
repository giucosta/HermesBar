using DAO.Connections;
using MODEL.Estabelecimento;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTIL;
using DAO.Utils;

namespace DAO.Estabelecimento
{
    public class ConfigEstabelecimentoDAO
    {
        public ConfigEstabelecimentoModel Salvar(ConfigEstabelecimentoModel ConfigEstabelecimento)
        {
            try
            {
                AccessObject<ConfigEstabelecimentoModel> AO = new AccessObject<ConfigEstabelecimentoModel>();
                AO.CreateDataInsert();
                AO.GetCommand();
                AO.InsertParameter("AgruparItensQuantidade", ConfigEstabelecimento.AgruparItensQuantidade);
                AO.InsertParameter("TipoSistema", ConfigEstabelecimento.TipoSistema);
                AO.InsertParameter("QuantidadeMesas", ConfigEstabelecimento.QuantidadeMesas);
                AO.InsertParameter("TaxaServico", ConfigEstabelecimento.TaxaServico);
                
                if(AO.ExecuteCommand())
                    return RecuperaConfigEstabelecimentoPeloId(RecuperaUltimoIdCadastrado());
                return null;
            }
            catch (Exception e)
            {
                Log.Log.GravarLog("Salvar","ConfigEstabelecimentoDAO",e.StackTrace,Constantes.ATipoMetodo.INSERT);
                return null;
            }
        }
        private int RecuperaUltimoIdCadastrado()
        {
            try
            {
                AccessObject<ConfigEstabelecimentoModel> AO = new AccessObject<ConfigEstabelecimentoModel>();
                AO.CreateSpecificQuery("SELECT MAX(Id_ConfigEstabelecimento) AS Id_ConfigEstabelecimento FROM ConfigEstabelecimento");
                AO.GetCommand();

                return (int)AO.GetDataTable().Rows[0]["Id_ConfigEstabelecimento"];
            }
            catch (Exception e)
            {
                Log.Log.GravarLog("RetornaConfigestabelecimento","ConfigEstabelecimentoDAO",e.StackTrace,Constantes.ATipoMetodo.SELECT);
                return 0;
            }
        }
        private ConfigEstabelecimentoModel RecuperaConfigEstabelecimentoPeloId(int id)
        {
            AccessObject<ConfigEstabelecimentoModel> AO = new AccessObject<ConfigEstabelecimentoModel>();
            AO.CreateSelectAll();
            AO.GetCommand();
            AO.InsertParameter(ConstantesDAO.WHERE, "Id_ConfigEstabelecimento", ConstantesDAO.EQUAL, id);

            return CarregaConfigEstabelecimento(AO.GetDataTable());
        }
        private ConfigEstabelecimentoModel CarregaConfigEstabelecimento(DataTable dataTable)
        {
            if (dataTable.Rows.Count != 0)
            {
                var configEstabelecimento = new ConfigEstabelecimentoModel();
                configEstabelecimento.Id = (int)dataTable.Rows[0]["Id_ConfigEstabelecimento"];
                configEstabelecimento.QuantidadeMesas = int.Parse(dataTable.Rows[0]["QuantidadeMesa"].ToString());
                configEstabelecimento.TaxaServico = int.Parse(dataTable.Rows[0]["TaxaServico"].ToString());
                if (dataTable.Rows[0]["AgruparItensQuantidade"].ToString().Equals("1"))
                    configEstabelecimento.AgruparItensQuantidade = true;
                else
                    configEstabelecimento.AgruparItensQuantidade = false;

                return configEstabelecimento;
            }
            return null;
        }
    }
}
