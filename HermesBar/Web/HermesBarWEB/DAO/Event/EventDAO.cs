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
                InserParameter("MTR", SqlDbType.Int, age.MTR);

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
        public DataTable Insert(HMA_AGE age)
        {
            try
            {
                OpenConnection();
                CreateDataAdapter(SQL.SP_AGE.INSERT);
                InserParameter("USR", SqlDbType.Int, age._USR);
                InserParameter("ATV", SqlDbType.Int, age._ATV);
                InserParameter("ID_CLI", SqlDbType.Int, age._ID_CLI);
                InserParameter("QUANT_RESR", SqlDbType.Int, age.QUANT_RESR);
                InserParameter("OBS", SqlDbType.VarChar, age.OBS);
                InserParameter("DATA_RESER", SqlDbType.DateTime, age.DATA_RESER);
                InserParameter("MTR", SqlDbType.Int, age.MTR);

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
        public DataTable Update(HMA_AGE age)
        {
            try
            {
                OpenConnection();
                CreateDataAdapter(SQL.SP_AGE.UPDATE);
                InserParameter("USR", SqlDbType.Int, age._USR);
                InserParameter("ATV", SqlDbType.Int, age._ATV);
                InserParameter("QUANT_RESR", SqlDbType.Int, age.QUANT_RESR);
                InserParameter("OBS", SqlDbType.VarChar, age.OBS);
                InserParameter("DATA_RESER", SqlDbType.DateTime, age.DATA_RESER);
                InserParameter("ID", SqlDbType.Int, age._ID);
                InserParameter("MTR", SqlDbType.Int, age.MTR);

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
