using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Employee
{
    public class TypeEmployeeDAO : Connection.Connection
    {
        public DataTable Get()
        {
            try
            {
                OpenConnection();
                CreateDataAdapter(SQL.SP_EMPL.GET_TYPE);

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
