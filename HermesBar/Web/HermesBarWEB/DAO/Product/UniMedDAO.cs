using ENTITY.Product;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Product
{
    public class UniMedDAO : Connection.Connection
    {
        public DataTable Get()
        {
            try
            {
                OpenConnection();
                CreateDataAdapter("[dbo].[SP_HMA_UNI_MED_GET]");

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
        public DataTable Insert(HMA_UNI_MED uniMed)
        {
            try
            {
                OpenConnection();
                CreateDataAdapter("[dbo].[SP_HMA_UNI_MED_INS]");
                InserParameter("USR", SqlDbType.Int, uniMed._USR);
                InserParameter("ATV", SqlDbType.Int, uniMed._ATV);
                InserParameter("NOM", SqlDbType.VarChar, uniMed.NOM);
                InserParameter("DSC", SqlDbType.VarChar, uniMed.DSC);

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
