using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Employee
{
    public class PlaceEmployeeDAO : Connection.Connection
    {
        public DataTable Get()
        {
            try
            {
                OpenConnection();
                CreateDataAdapter(SQL.SP_EMPL.GET_CAR);

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
