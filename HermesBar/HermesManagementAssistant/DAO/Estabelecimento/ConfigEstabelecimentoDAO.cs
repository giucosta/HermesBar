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

                return RecuperaConfigEstabelecimentoPeloId(RecuperaUltimoIdCadastrado());
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
                var sql = "SELECT MAX(Id_ConfigEstabelecimento) as Id_ConfigEstabelecimento FROM ConfigEstabelecimento";
                var comando = new SqlCommand(sql, Connection.GetConnection());

                return (int)Connection.getDataTable(comando).Rows[0]["Id_ConfigEstabelecimento"];
            }
            catch (Exception e)
            {
                Log.Log.GravarLog("RetornaConfigestabelecimento","ConfigEstabelecimentoDAO",e.StackTrace,Constantes.ATipoMetodo.SELECT);
                return 0;
            }
        }
        private ConfigEstabelecimentoModel RecuperaConfigEstabelecimentoPeloId(int id)
        {
            var sql = "SELECT * FROM ConfigEstabelecimento WHERE Id_ConfigEstabelecimento = @id";
            var comando = new SqlCommand(sql, Connection.GetConnection());
            comando.Parameters.AddWithValue("@id",id);

            return CarregaConfigEstabelecimento(Connection.getDataTable(comando));
        }
        private ConfigEstabelecimentoModel CarregaConfigEstabelecimento(DataTable dataTable)
        {
            if (dataTable.Rows.Count != 0)
            {
                var configEstabelecimento = new ConfigEstabelecimentoModel();
                configEstabelecimento.Id = (int)dataTable.Rows[0]["Id_ConfigEstabelecimento"];
                configEstabelecimento.QuantidadeMesas = (int)dataTable.Rows[0]["QuantidadeMesa"];
                configEstabelecimento.TaxaServico = (int)dataTable.Rows[0]["TaxaServico"];
                if ((int)dataTable.Rows[0]["AgruparItensQuantidade"] == 1)
                    configEstabelecimento.AgruparItensQuantidade = true;
                else
                    configEstabelecimento.AgruparItensQuantidade = false;

                return configEstabelecimento;
            }
            return null;
        }
    }
}
