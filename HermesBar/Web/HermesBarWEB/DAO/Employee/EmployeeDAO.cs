using ENTITY.Employee;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Employee
{
    public class EmployeeDAO : Connection.Connection
    {
        public DataSet Get(HMA_FUNC func)
        {
            try
            {
                OpenConnection();
                CreateDataAdapter(SQL.SP_EMPL.GET);
                InserParameter("ID", SqlDbType.Int, func._ID);

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
