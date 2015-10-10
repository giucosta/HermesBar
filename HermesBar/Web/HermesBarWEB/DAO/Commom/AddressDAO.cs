using ENTITY.Commom;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Commom
{
    public class AddressDAO : Connection.Connection
    {
        public DataTable GetStates()
        {
            try
            {
                OpenConnection();
                var data = new DataTable();
                CreateDataAdapter(SQL.SP_ADDRESS.GET_UF);

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
