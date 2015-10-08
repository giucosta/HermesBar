using DAO.SQL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.User
{
    public class ProfileDAO : Connection.Connection
    {
        public DataTable Get()
        {
            try
            {
                OpenConnection();
                CreateDataAdapter(SP_PER.GET);

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
