using DAO.UTIL;
using ENTITY.Commom;
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
        public DataTable Insert(HMA_FUNC func, HMA_CON con, HMA_END end)
        {
            try
            {
                OpenConnection();
                CreateDataAdapter(SQL.SP_EMPL.INSERT);
                InserParameter("USR", SqlDbType.Int, func._USR);
                InserParameter("ATV", SqlDbType.Int, func._ATV);
                InserParameter("RG", SqlDbType.Int, func.RG);
                InserParameter("CPF", SqlDbType.Int, func.CPF);
                InserParameter("CTPS", SqlDbType.Int, func.CTPS);
                InserParameter("DT_ADM", SqlDbType.Int, func.DT_ADM);
                InserParameter("DT_DEM", SqlDbType.Int, func.DT_DEM);
                InserParameter("TIP", SqlDbType.Int, func.TIP);
                InserParameter("ID_CAR", SqlDbType.Int, func._ID_CAR);
                InserParameter("FUN", SqlDbType.VarChar, func.FUN);
                InserParameter("SEX", SqlDbType.Char, func.SEX);

                new UTIL.ContactParameters().InsertContactParameters(ref con);
                new UTIL.AddressParameters().InsertAddressParameters(ref end);
                
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
