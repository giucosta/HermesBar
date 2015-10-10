using DAO.SQL;
using ENTITY.PDV;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.PDV
{
    public class Pdv_OrderDAO : Connection.Connection
    {
        public DataTable Insert(HMA_PDV_PED_CLI pedido)
        {
            try
            {
                OpenConnection();
                CreateDataAdapter(SP_PDV_CLI.INSERT_PEDIDO);
                InserParameter("NUM_CAR", SqlDbType.Int, pedido._ID_CLI);
                InserParameter("NOM_PROD", SqlDbType.VarChar, pedido._ID_PROD);
                InserParameter("ID_FUNC", SqlDbType.Int, pedido._ID_FUNC);
                InserParameter("QTD", SqlDbType.Int, pedido.QTD);
                InserParameter("USR", SqlDbType.Int, pedido._USR);
                InserParameter("CAI", SqlDbType.Int, pedido.CAI);

                return GetResult();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                CloseConnection();
            }
        }
    }
}
