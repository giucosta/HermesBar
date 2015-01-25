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
    public class ConfigEstabelecimentoDAO
    {
        public bool Salvar(ConfigEstabelecimentoModel configEstabelecimento)
        {
            try
            {
                var sql = @"INSERT INTO ConfigEstabelecimento VALUES(@AgruparItensQuantidade,@TipoSistema,@QuantidadeMesa,@TaxaServico,@Id_Estabelecimento)";
                var comando = new SqlCommand(sql,Connection.GetConnection());
                comando.Parameters.AddWithValue("@AgruparItensQuantidade",configEstabelecimento.AgruparItensQuantidade);
                comando.Parameters.AddWithValue("@TipoSistema",configEstabelecimento.TipoSistema);
                comando.Parameters.AddWithValue("@QuantidadeMesa",configEstabelecimento.QuantidadeMesas);
                comando.Parameters.AddWithValue("@TaxaServico",configEstabelecimento.TaxaServico);
                comando.Parameters.AddWithValue("Id_Estabelecimento",configEstabelecimento.Estabelecimento.Id);

                Connection.ExecutarComando(comando);
                return true;
            }
            catch (Exception e)
            {
                Log.Log.GravarLog("Salvar","ConfigEstabelecimentoDAO",e.StackTrace,Constantes.ATipoMetodo.INSERT);
                return false;
            }
        }
    }
}
