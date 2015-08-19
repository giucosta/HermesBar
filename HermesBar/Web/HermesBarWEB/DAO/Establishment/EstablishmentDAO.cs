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
        public DataTable Get()
        {
            try
            {
                OpenConnection();
                CreateDataAdapter("[dbo].[SP_HMA_EST_GET]");

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
