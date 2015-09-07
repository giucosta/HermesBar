using ENTITY.PDV;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.PDV
{
    public class Pdv_CaixaDAO : Connection.Connection
    {
        public DataTable Open(HMA_PDV_CAI caixa)
        {
            try
            {
                OpenConnection();
                CreateDataAdapter(SQL.SP_PDV_CAI.OPEN);
                InserParameter("USR", SqlDbType.Int, caixa._USR);
                InserParameter("ATV",SqlDbType.Int, caixa._ATV);
                InserParameter("DT_ABER",SqlDbType.DateTime, caixa.DT_ABER);
                InserParameter("DT_FEC",SqlDbType.DateTime, caixa.DT_FEC);
                InserParameter("VLR_INI",SqlDbType.Decimal, caixa.VLR_INI);
                InserParameter("VLR_FIN", SqlDbType.Decimal, caixa.VLR_FIN);

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
