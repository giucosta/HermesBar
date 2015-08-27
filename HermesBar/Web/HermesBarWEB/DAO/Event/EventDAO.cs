using ENTITY.Event;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Event
{
    public class EventDAO : Connection.Connection
    {
        public DataTable Get(HMA_AGE age)
        {
            try
            {
                OpenConnection();
                CreateDataAdapter(SQL.SP_AGE.GET);
                InserParameter("ID", SqlDbType.Int, age._ID);

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
