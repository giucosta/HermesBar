using DAO.Connections;
using MODEL.Estabelecimento;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTILS;

namespace DAO.Estabelecimento
{
    public class ConfigEstabelecimentoDAO
    {
        public ConfigEstabelecimentoModel Salvar(ConfigEstabelecimentoModel configEstabelecimento)
        {
            try
            {
                var sql = @"INSERT INTO ConfigEstabelecimento VALUES(@AgruparItensQuantidade,@TipoSistema,@QuantidadeMesa,@TaxaServico)";
                var comando = new SqlCommand(sql,Connection.GetConnection());
                comando.Parameters.AddWithValue("@AgruparItensQuantidade",configEstabelecimento.AgruparItensQuantidade);
                comando.Parameters.AddWithValue("@TipoSistema",configEstabelecimento.TipoSistema);
                comando.Parameters.AddWithValue("@QuantidadeMesa",configEstabelecimento.QuantidadeMesas);
                comando.Parameters.AddWithValue("@TaxaServico",configEstabelecimento.TaxaServico);

                Connection.ExecutarComando(comando);

                return RetornaConfigEstabelecimento(configEstabelecimento);
            }
            catch (Exception e)
            {
                Log.Log.GravarLog("Salvar","ConfigEstabelecimentoDAO",e.StackTrace,Constantes.ATipoMetodo.INSERT);
                return null;
            }
        }

        private ConfigEstabelecimentoModel RetornaConfigEstabelecimento(ConfigEstabelecimentoModel configEstabelecimento)
        {
            try
            {
                var sql = "SELECT MAX(Id_ConfigEstabelecimento) FROM ConfigEstabelecimento";
                var comando = new SqlCommand(sql, Connection.GetConnection());
                
                return CarregaConfigEstabelecimento(Connection.getDataTable(comando));
            }
            catch (Exception e)
            {
                Log.Log.GravarLog("RetornaConfigestabelecimento","ConfigEstabelecimentoDAO",e.StackTrace,Constantes.ATipoMetodo.SELECT);
                return null;
            }
        }

        private ConfigEstabelecimentoModel CarregaConfigEstabelecimento(DataTable dataTable)
        {
            if (dataTable.Rows.Count != 0)
            {
                var configEstabelecimento = new ConfigEstabelecimentoModel();
                configEstabelecimento.Id = (int)dataTable.Rows[0]["Id_ConfigEstabelecimento"];
                configEstabelecimento.QuantidadeMesas = (int)dataTable.Rows[0]["QuantidadeMesa"];
                configEstabelecimento.TaxaServico = (int)dataTable.Rows[0]["TaxaServico"];

                return configEstabelecimento;
            }
            return null;
        }
    }
}
