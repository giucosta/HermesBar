using ENTITY.Product;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Product
{
    public class ProductDAO : Connection.Connection
    {
        public DataTable Get()
        {
            try
            {
                OpenConnection();
                var data = new DataTable();
                CreateDataAdapter("[dbo].[SP_HMA_PROD_GET]");

                return GetResult();
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

        public DataTable GetId(HMA_PROD prod)
        {
            try
            {
                OpenConnection();
                CreateDataAdapter("SP_HMA_PROD_GETID");
                InserParameter("ID", SqlDbType.Int, prod._ID);

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
        public DataTable Insert(HMA_PROD prod, HMA_TIP tip, HMA_UNI_MED med)
        {
            try
            {
                OpenConnection();
                CreateDataAdapter("SP_HMA_PROD_INS");
                InserParameter("USR", SqlDbType.Int, prod._USR);
                InserParameter("ATV", SqlDbType.Int, prod._ATV);
                InserParameter("NOM", SqlDbType.VarChar, prod.NOM);
                InserParameter("DSC", SqlDbType.VarChar, prod.DSC);
                InserParameter("VLR_COM", SqlDbType.Decimal, prod.VLR_COM);
                InserParameter("VLR_VEN", SqlDbType.Decimal, prod.VLR_VEN);
                InserParameter("COD_VEN", SqlDbType.VarChar, prod.COD_VEN);
                InserParameter("QUANT_MIN", SqlDbType.Int, prod.QUANT_MIN);
                InserParameter("ID_TIP", SqlDbType.Int, tip._ID);
                InserParameter("ID_MED", SqlDbType.Int, med._ID);
                InserParameter("QUANT_ATL", SqlDbType.Int, prod.QUANT_ATL);

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
        public DataTable GetNextCod()
        {
            try
            {
                OpenConnection();
                CreateDataAdapter("SP_HMA_PROD_GET_COD");

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
        public DataTable VerifyExistingProduct(HMA_PROD prod)
        {
            try
            {
                OpenConnection();
                CreateDataAdapter("SP_HMA_PROD_VER_PROD");
                InserParameter("COD_VEN", SqlDbType.VarChar, prod.COD_VEN);
                InserParameter("NOM", SqlDbType.VarChar, prod.NOM);

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
