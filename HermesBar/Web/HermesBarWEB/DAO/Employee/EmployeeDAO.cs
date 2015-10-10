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
                InserParameter("CPF", SqlDbType.VarChar, func.CPF);

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
                InserParameter("RG", SqlDbType.VarChar, func.RG);
                InserParameter("CPF", SqlDbType.VarChar, func.CPF);
                InserParameter("CTPS", SqlDbType.VarChar, func.CTPS);
                InserParameter("DT_ADM", SqlDbType.DateTime, func.DT_ADM);
                InserParameter("DT_DEM", SqlDbType.DateTime, func.DT_DEM);
                InserParameter("TIP", SqlDbType.Int, func.TIP);
                InserParameter("ID_CAR", SqlDbType.Int, func._ID_CAR);
                InserParameter("FUN", SqlDbType.VarChar, func.FUN);
                InserParameter("SEX", SqlDbType.Char, func.SEX);

                InserParameter("RUA", SqlDbType.VarChar, end.RUA);
                InserParameter("NUM", SqlDbType.VarChar, end.NUM);
                InserParameter("BAI", SqlDbType.VarChar, end.BAI);
                InserParameter("COMP", SqlDbType.VarChar, end.COMP);
                InserParameter("CEP", SqlDbType.VarChar, end.CEP);
                InserParameter("CID", SqlDbType.VarChar, end.CID);
                InserParameter("UF", SqlDbType.Int, end.UF);

                InserParameter("NOM", SqlDbType.VarChar, con.NOM);
                InserParameter("TEL", SqlDbType.VarChar, con.TEL);
                InserParameter("CEL", SqlDbType.VarChar, con.CEL);
                InserParameter("EMA", SqlDbType.VarChar, con.EMA);

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
        public DataTable Update(HMA_FUNC func, HMA_CON con, HMA_END end)
        {
            OpenConnection();

            CreateDataAdapter(SQL.SP_EMPL.UPDATE);
            InserParameter("ID", SqlDbType.Int, func._ID);
            InserParameter("USR", SqlDbType.Int, func._USR);
            InserParameter("ATV", SqlDbType.Int, func._ATV);
            InserParameter("RG", SqlDbType.VarChar, func.RG);
            InserParameter("CPF", SqlDbType.VarChar, func.CPF);
            InserParameter("CTPS", SqlDbType.VarChar, func.CTPS);
            InserParameter("DT_ADM", SqlDbType.DateTime, func.DT_ADM);
            InserParameter("DT_DEM", SqlDbType.DateTime, func.DT_DEM);
            InserParameter("TIP", SqlDbType.Int, func.TIP);
            InserParameter("ID_CAR", SqlDbType.Int, func._ID_CAR);
            InserParameter("FUN", SqlDbType.VarChar, func.FUN);
            InserParameter("SEX", SqlDbType.Char, func.SEX);

            InserParameter("RUA", SqlDbType.VarChar, end.RUA);
            InserParameter("NUM", SqlDbType.VarChar, end.NUM);
            InserParameter("BAI", SqlDbType.VarChar, end.BAI);
            InserParameter("COMP", SqlDbType.VarChar, end.COMP);
            InserParameter("CEP", SqlDbType.VarChar, end.CEP);
            InserParameter("CID", SqlDbType.VarChar, end.CID);
            InserParameter("UF", SqlDbType.Int, end.UF);

            InserParameter("NOM", SqlDbType.VarChar, con.NOM);
            InserParameter("TEL", SqlDbType.VarChar, con.TEL);
            InserParameter("CEL", SqlDbType.VarChar, con.CEL);
            InserParameter("EMA", SqlDbType.VarChar, con.EMA);

            return GetResult();
        }
        public DataTable Inactive(HMA_FUNC func)
        {
            try
            {
                OpenConnection();

                CreateDataAdapter(SQL.SP_EMPL.INACTIVE);
                InserParameter("ID", SqlDbType.Int, func._ID);
                InserParameter("USR", SqlDbType.Int, func._USR);

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
        public DataTable Active(HMA_FUNC func)
        {
            try
            {
                OpenConnection();

                CreateDataAdapter(SQL.SP_EMPL.ACTIVE);
                InserParameter("ID", SqlDbType.Int, func._ID);
                InserParameter("USR", SqlDbType.Int, func._USR);

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
