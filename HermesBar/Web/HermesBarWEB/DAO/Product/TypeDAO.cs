using ENTITY.Product;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Product
{
    public class TypeDAO : Connection.Connection
    {
        public DataTable Get()
        {
            try
            {
                OpenConnection();
                var data = new DataTable();
                CreateDataAdapter("[dbo].[SP_HMA_TIP_GET]");

                return GetResult(data);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
            }
        }
        public DataTable Insert(HMA_TIP tipo)
        {
            try
            {
                OpenConnection();
                var data = new DataTable();
                CreateDataAdapter("[dbo].[SP_HMA_TIP_INS]");
                InserParameter("USR", SqlDbType.Int, tipo._USR);
                InserParameter("ATV", SqlDbType.Int, tipo._ATV);
                InserParameter("NOM", SqlDbType.VarChar, tipo.NOM);
                InserParameter("DSC", SqlDbType.VarChar, tipo.DSC);

                return GetResult(data);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
            }
        }
    }
}
