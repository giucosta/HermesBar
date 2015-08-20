using ENTITY.Establishment;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Establishment
{
    public class EstablishmentDAO : Connection.Connection
    {
        public DataSet Get(HMA_EST est)
        {
            try
            {
                OpenConnection();
                CreateDataAdapter(SQL.SP_EST.GET);
                InserParameter("ID_EST", SqlDbType.Int, est._ID);

                return GetResultAsDataSet();
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
