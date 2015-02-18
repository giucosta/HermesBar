using DAO.Connections;
using MODEL.Estabelecimento;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO;
using DAO.Utils;

namespace DAO.Estabelecimento
{
    public class ConfigEstabelecimentoDAO
    {
        public ConfigEstabelecimentoModel Salvar(ConfigEstabelecimentoModel ConfigEstabelecimento)
        {
            try
            {
                var sql = AccessObject<ConfigEstabelecimentoModel>.CreateDataInsert();
                var comando = new SqlCommand(sql,Connection.GetConnection());
                comando.Parameters.AddWithValue("@AgruparItensQuantidade",ConfigEstabelecimento.AgruparItensQuantidade);
                comando.Parameters.AddWithValue("@TipoSistema",ConfigEstabelecimento.TipoSistema);
                comando.Parameters.AddWithValue("@QuantidadeMesa",ConfigEstabelecimento.QuantidadeMesas);
                comando.Parameters.AddWithValue("@TaxaServico",ConfigEstabelecimento.TaxaServico);

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
                var sql = AccessObject<ConfigEstabelecimentoModel>.InsertParameter("",ConstantesDAO.SELECT,ConstantesDAO.MAX);
                sql = AccessObject<ConfigEstabelecimentoModel>.InsertParameter(sql,"(Id_ConfigEstabelecimento)",ConstantesDAO.AS);
                sql = AccessObject<ConfigEstabelecimentoModel>.InsertParameter(sql, "Id_ConfigEstabelecimento",ConstantesDAO.FROM);
                sql = AccessObject<ConfigEstabelecimentoModel>.InsertSimpleParameter(sql,"ConfigEstabelecimento");
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
            var sql = AccessObject<ConfigEstabelecimentoModel>.CreateSelectAll();
            sql = AccessObject<ConfigEstabelecimentoModel>.InsertParameter(sql,ConstantesDAO.WHERE, "Id_ConfigEstabelecimento");
            sql = AccessObject<ConfigEstabelecimentoModel>.InsertParameter(sql, ConstantesDAO.EQUAL, "id");
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
        public bool SalvarConfigEstabelecimentoEstabelecimento(EstabelecimentoModel estabelecimento)
        {
            return new ConfigEstabelecimentoEstabelecimentoDAO().Salvar(estabelecimento);
        }
    }
}
