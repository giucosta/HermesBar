using DAO.Connections;
using MODEL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTILS;

namespace DAO.Comum
{
    public class TipoEnderecoDAO
    {
        public TipoEnderecoModel RetornaTipoEndereco(EnderecoModel endereco)
        {
            try
            {
                var sql = @"SELECT * FROM TipoEndereco WHERE Tipo = @Tipo";
                var comando = new SqlCommand(sql, Connection.GetConnection());
                comando.Parameters.AddWithValue("@Tipo", endereco.Tipo);

                var dataTable = Connection.getDataTable(comando);

                return new TipoEnderecoModel()
                {
                    Id = (int)dataTable.Rows[0]["Id_TipoEndereco"],
                    Tipo = dataTable.Rows[0]["Tipo"].ToString()
                };
            }
            catch (Exception e)
            {
                Log.Log.GravarLog("RetornaTipoEndereco","TipoEnderecoDAO",e.StackTrace,Constantes.ATipoMetodo.SELECT);
                return null;
            }
            
        }
    }
}
